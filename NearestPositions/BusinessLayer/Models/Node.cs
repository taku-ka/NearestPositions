using NearestPositions.Helpers.Utilties;

namespace NearestPositions.BusinessLayer.Models
{
    /// <summary>
    /// Model for Node
    /// </summary>
    public class Node
    {
        internal int axis;

        internal double[] x;

        internal int nodeId;
        internal bool seenNode;

        internal Node Parent, Left, Right;

        public Node(double[] x0, int axis0)
        {
            x = new double[2];
            axis = axis0;
            for (int k = 0; k < 2; k++)
                x[k] = x0[k];

            Left = Right = Parent = null;
            seenNode = false;
            nodeId = 0;
        }

        public Node FindParent(double[] x0)
        {
            Node parent = null;
            Node next = this;
            int split;
            while (next != null)
            {
                split = next.axis;
                parent = next;
                if (x0[split] > next.x[split])
                    next = next.Right;
                else
                    next = next.Left;
            }
            return parent;
        }

        public Node Insert(double[] p)
        {
            Node parentNode = FindParent(p);
            if (Equal(p, parentNode.x, 2) == true)
                return null;

            Node newNode = new Node(p, parentNode.axis + 1 < 2 ? parentNode.axis + 1
                    : 0);
            newNode.Parent = parentNode;

            if (p[parentNode.axis] > parentNode.x[parentNode.axis])
            {
                parentNode.Right = newNode;
            }
            else
            {
                parentNode.Left = newNode;
            }

            return newNode;
        }

        internal bool Equal(double[] x1, double[] x2, int dim)
        {
            for (int k = 0; k < dim; k++)
            {
                if (x1[k] != x2[k])
                    return false;
            }

            return true;
        }

        internal double Distance2(double[] x1, double[] x2)
        {
            double s2 = CalculatorUtilities.DistanceCalculator(
                 new Location { Latitude = x1[0], Longitude = x1[1] },
                 new Location { Latitude = x2[0], Longitude = x2[1] });

            return Convert.ToSingle(s2);
        }
    }

}