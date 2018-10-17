using log4net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoundingBoxesDetector
{
    public class BoundingBoxesUtil
    {
        private static ILog logger = LogManager.GetLogger(typeof(BoundingBoxesUtil));

        #region "utility initiation"

        private static readonly Lazy<BoundingBoxesUtil> _instance = new Lazy<BoundingBoxesUtil>(() => new BoundingBoxesUtil());

        private BoundingBoxesUtil() { logger.Debug("initiating boudning box utility..."); }

        public static BoundingBoxesUtil Instance { get { return _instance.Value; } }

        #endregion

        public List<BoundingBox> GetLargestValidBoundingBoxes(string filePath)
        {
            char[][] inputMap = GetInputMap(filePath);
            List<BoundingBox> candidates = GetAllBoundingBoxes(inputMap);
            return GetLargestValidBoundingBoxesFromCandidates(candidates);
        }

        /// <summary>
        /// 3. eliminate overlapping bounding boxes and the largest one
        /// </summary>
        /// <param name="candidates">all bounding boxes in the input map</param>
        /// <returns></returns>
        private List<BoundingBox> GetLargestValidBoundingBoxesFromCandidates(List<BoundingBox> candidates)
        {
            logger.DebugFormat("eliminating overlapping bounding boxes and get the largest one from the {0} candidate(s)", candidates.Count);

            List<BoundingBox> result = new List<BoundingBox>();
            int maxArea = 0;

            while (candidates.Count != 0)
            {
                BoundingBox candidate = candidates[0];
                candidates.RemoveAt(0);
                bool valid = true;
                for (int i = 0; i < candidates.Count; ++i)
                {
                    // if candidate bounding box is overlaping with any other bounding boxes, remove the other bounding box too and break to the next candidate bounding box
                    if (candidate.IsOverlapWith(candidates[i]))
                    {
                        candidates.RemoveAt(i);
                        valid = false;
                        break;
                    }
                }

                // if candidate is valid, calculate the area and update the result if it's the maximum area
                if (valid)
                {
                    int area = candidate.GetArea();
                    if (area == maxArea)
                    {
                        result.Add(candidate);
                    }
                    else if (area > maxArea)
                    {
                        maxArea = area;
                        result = new List<BoundingBox>();
                        result.Add(candidate);
                    }
                }
            }

            logger.DebugFormat("found {0} largest valid bounding boxes", result.Count);
            return result;
        }

        /// <summary>
        /// 2. return all bounding boxes in the input map
        /// </summary>
        /// <param name="inputMap">the input map in a char array</param>
        /// <returns></returns>
        private List<BoundingBox> GetAllBoundingBoxes(char[][] inputMap)
        {
            List<BoundingBox> result = new List<BoundingBox>();
            if (inputMap.Length == 0)
            {
                return result;
            }

            logger.DebugFormat("getting all bounding boxes in the input map (row#: {0}, col#: {1})", inputMap.Length, inputMap[0].Length);

            // walk the input map using DFS and get all bounding boxes
            for (int i = 0; i < inputMap.Length; ++i)
            {
                for (int j = 0; j < inputMap[0].Length; ++j)
                {
                    BoundingBox box = new BoundingBox();
                    WalkMap(inputMap, i, j, box);
                    if (box.upperLeftRow != 0)
                    {
                        result.Add(box);
                        logger.InfoFormat("bonding box found @({0},{1})({2},{3})", box.upperLeftRow, box.upperLeftCol, box.lowerRightRow, box.lowerRightCol);
                    }
                }
            }

            logger.DebugFormat("found {0} bounding boxes", result.Count);
            return result;
        }

        /// <summary>
        /// 2.1 walk the input map and get the first encountered bounding box
        /// change all walked pixels to '-'
        /// </summary>
        /// <param name="inputMap">the input map in a char array</param>
        /// <param name="i">current row index</param>
        /// <param name="j">current column index</param>
        /// <param name="box">the first encountered bounding box</param>
        private void WalkMap(char[][] inputMap, int i, int j, BoundingBox box)
        {
            // check if walked outside of boundries
            if (i < 0 || j < 0 || i >= inputMap.Length || j >= inputMap[0].Length)
            {
                return;
            }

            // get the boundry of the bounding box if any
            if (inputMap[i][j] == '*')
            {
                // if the new dot is outside of the current bounding box's edges, update the current boudning box's edges
                box.UpdateEdges(i + 1, j + 1);

                // change the current node to '-' and walk the adjacent nodes
                inputMap[i][j] = '-';
                WalkMap(inputMap, i, j - 1, box);
                WalkMap(inputMap, i - 1, j, box);
                WalkMap(inputMap, i, j + 1, box);
                WalkMap(inputMap, i + 1, j, box);
            }
        }

        /// <summary>
        /// 1. read input text file and generate an input map using a char array
        /// </summary>
        /// <param name="path">path of the input text file</param>
        /// <returns></returns>
        private char[][] GetInputMap(string path)
        {
            logger.DebugFormat("reading input map @{0}", path);

            string[] input;
            char[][] output;

            try
            {
                input = File.ReadAllLines(path);

                // check if file has contents
                int row = input.Length;
                if (row == 0)
                {
                    return new char[0][];
                }
                int col = input[0].Length;

                output = new char[row][];
                for (int i = 0; i < input.Length; ++i)
                {
                    if (input[i].Length != col)
                    {
                        // check if each line in the input file has the same # of chars
                        throw new ApplicationException(String.Format("line #{0} has {1} char(s), does not match the column length {2}", i, input[i].Length, col));
                    }

                    output[i] = input[i].ToCharArray();
                }

                logger.DebugFormat("constructed an input map with {0} of row(s) and {1} of col(s)", output.Length, output.Length == 0 ? "n/a" : output[0].Length.ToString());
                return output;
            }
            catch (FileNotFoundException e)
            {
                logger.ErrorFormat("file does not exist @{0}", path);
                logger.ErrorFormat("{0}\n\t{1}", e.Message, e.StackTrace);
                throw;
            }
            catch (Exception e)
            {
                logger.ErrorFormat("{0}\n\t{1}", e.Message, e.StackTrace);
                throw;
            }
        }
    }
}
