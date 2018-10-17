using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoundingBoxesDetector
{
    public class BoundingBox
    {
        private static ILog logger = LogManager.GetLogger(typeof(BoundingBox));

        public int upperLeftRow { get; set; }
        public int upperLeftCol { get; set; }
        public int lowerRightRow { get; set; }
        public int lowerRightCol { get; set; }

        public BoundingBox()
        {
            logger.DebugFormat("Initiating an empty [{0}]", typeof(BoundingBox));

            this.upperLeftRow = 0;
            this.upperLeftCol = 0;
            this.lowerRightRow = 0;
            this.lowerRightCol = 0;
        }

        public BoundingBox(int upperLeftRow, int upperLeftCol, int lowerRightRow, int lowerRightCol)
        {
            logger.DebugFormat("Initiating a [{0}] with coordinates: ({1},{2})({3},{4})", typeof(BoundingBox), upperLeftRow, upperLeftCol, lowerRightRow, lowerRightCol);

            this.upperLeftRow = upperLeftRow;
            this.upperLeftCol = upperLeftCol;
            this.lowerRightRow = lowerRightRow;
            this.lowerRightCol = lowerRightCol;
        }

        /// <summary>
        /// update the box's edges with the new point
        /// </summary>
        /// <param name="row">row# of the new point</param>
        /// <param name="col">col# of the new point</param>
        public void UpdateEdges(int row, int col)
        {
            logger.DebugFormat("checking if point ({0}, {1}) is out side of current bounding box @({2},{3})({4},{5})", row, col, this.upperLeftRow, this.upperLeftCol, this.lowerRightRow, this.lowerRightCol);

            if (this.upperLeftRow == 0 || this.upperLeftRow > row)
            {
                this.upperLeftRow = row;
            }
            if (this.upperLeftCol == 0 || this.upperLeftCol > col)
            {
                this.upperLeftCol = col;
            }
            if (this.lowerRightRow == 0 || this.lowerRightRow < row)
            {
                this.lowerRightRow = row;
            }
            if (this.lowerRightCol == 0 || this.lowerRightCol < col)
            {
                this.lowerRightCol = col;
            }

            logger.DebugFormat("bounding box edges updated to ({0},{1})({2},{3})", this.upperLeftRow, this.upperLeftCol, this.lowerRightRow, this.lowerRightCol);
        }

        /// <summary>
        /// check if the box is overlapping with another box
        /// </summary>
        /// <param name="box">the other box</param>
        /// <returns></returns>
        public bool IsOverlapWith(BoundingBox box)
        {
            logger.DebugFormat("checking if current bounding box @({0},{1})({2},{3}) is overlap with bounding box @({4},{5})({6},{7})", this.upperLeftRow, this.upperLeftCol, this.lowerRightRow, this.lowerRightCol, box.upperLeftRow, box.upperLeftCol, box.lowerRightRow, box.lowerRightCol);

            bool rowOverlap = (box.upperLeftRow >= this.upperLeftRow) && (box.upperLeftRow <= this.lowerRightRow)
                || ((box.lowerRightRow >= this.upperLeftRow) && (box.lowerRightRow <= this.lowerRightRow));
            bool colOverlap = ((box.upperLeftCol >= this.upperLeftCol) && (box.upperLeftCol <= this.lowerRightCol))
                || ((box.lowerRightCol >= this.upperLeftCol) && (box.lowerRightCol <= this.lowerRightCol));

            logger.DebugFormat("bounding box @({0},{1})({2},{3}) {8} overlapping with bounding box @({4},{5})({6},{7})", this.upperLeftRow, this.upperLeftCol, this.lowerRightRow, this.lowerRightCol, box.upperLeftRow, box.upperLeftCol, box.lowerRightRow, box.lowerRightCol, rowOverlap && colOverlap ? "is" : "is not");

            return rowOverlap && colOverlap;
        }

        /// <summary>
        /// calculate the box's area
        /// </summary>
        /// <returns></returns>
        public int GetArea()
        {
            logger.DebugFormat("calculating the current bounding box's area");

            int area = (this.lowerRightRow - this.upperLeftRow + 1) * (this.lowerRightCol - this.upperLeftCol + 1);

            logger.DebugFormat("the box takes {0} of spaces", area);
            return area;
        }

        public override string ToString()
        {
            return String.Format("({0},{1})({2},{3})", this.upperLeftRow, this.upperLeftCol, this.lowerRightRow, this.lowerRightCol);
        }

        /// <summary>
        /// overriding for unit testing purpose
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            BoundingBox item = obj as BoundingBox;

            if (this.upperLeftRow == item.upperLeftRow && this.upperLeftCol == item.upperLeftCol && this.lowerRightRow == item.lowerRightRow && this.lowerRightCol == item.lowerRightCol)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// overriding for unit testing purpose
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return String.Format("{0},{1},{2},{3}", this.upperLeftRow, this.upperLeftCol, this.lowerRightRow, this.lowerRightCol).GetHashCode();
        }
    }
}
