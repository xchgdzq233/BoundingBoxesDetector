using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BoundingBoxesDetector
{
    public class Program
    {
        private static ILog logger = LogManager.GetLogger(typeof(Program));

        [STAThread]
        public static void Main(string[] args)
        {
            string filePath = "";

            if (args.Length == 1)
            {
                filePath = args[0].Trim();
                if (!filePath.EndsWith(".txt"))
                {
                    filePath = "";
                }
            }
            else if (args.Length == 0)
            {
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Multiselect = false;
                dialog.Title = "Select the input file";
                dialog.Filter = "(Input File)|*.txt";
                dialog.InitialDirectory = Environment.CurrentDirectory;

                if (dialog.ShowDialog() != DialogResult.OK)
                {
                    Environment.Exit(0);
                }
                filePath = dialog.FileName;
            }

            if (String.IsNullOrEmpty(filePath))
            {
                logger.ErrorFormat("invalid input file");
                Environment.Exit(1);
            }

            logger.InfoFormat("intput file: {0}", filePath);

            try
            {
                List<BoundingBox> boxes = BoundingBoxesUtil.Instance.GetLargestValidBoundingBoxes(filePath);
                foreach(BoundingBox box in boxes)
                {
                    Console.WriteLine(box.ToString());
                }

                Environment.Exit(0);
            }
            catch(Exception e)
            {
                logger.ErrorFormat("{0}\n\t{1}", e.Message, e.StackTrace);
                Environment.Exit(1);
            }
        }
    }
}
