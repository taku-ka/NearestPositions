using NearestPositions.BusinessLayer.Models;

namespace NearestPositions.Helpers.Utilties
{
    public class KdTree
    {
        Node RootNode;
        double minDistance;
        Node nearestNeighbour;

        int KdId, nList;

        Node[] seenNodes;
        int seenNode;


        Node[] NodesList;

        double[] xMin, xMax;

        int nodeBoundary;
        bool[] maxBoundary, minBoundary;

        public KdTree(int i)
        {
            RootNode = null;
            KdId = 1;
            nList = 0;
            NodesList = new Node[i];
            seenNodes = new Node[i];
            maxBoundary = new bool[2];
            minBoundary = new bool[2];
            xMin = new double[2];
            xMax = new double[2];
        }

        public bool Add(double[] x)
        {
            x[0] = Math.Round(x[0], 5);
            x[1] = Math.Round(x[1], 5);

            if (nList >= 2000000 - 1)
                return false;

            if (RootNode == null)
            {
                RootNode = new Node(x, 0);
                RootNode.nodeId = KdId++;
                NodesList[nList++] = RootNode;
            }
            else
            {
                Node pNode;
                if ((pNode = RootNode.Insert(x)) != null)
                {
                    pNode.nodeId = KdId++;
                    NodesList[nList++] = pNode;
                }
            }

            return true;
        }

        public Node? FindNearest(double[] x)
        {
            if (RootNode == null)
                return null;

            seenNode = 0;
            Node parent = RootNode.FindParent(x);
            nearestNeighbour = parent;
            minDistance = RootNode.Distance2(x, parent.x);


            if (parent.Equal(x, parent.x, 2) == true)
                return nearestNeighbour;

            SearchParent(parent, x);
            Uncheck();

            return nearestNeighbour;
        }

        public void CheckSubtree(Node node, double[] x)
        {
            if ((node == null) || node.seenNode)
                return;

            seenNodes[seenNode++] = node;
            node.seenNode = true;
            SetBoundingCube(node, x);

            int dim = node.axis;
            double d = node.x[dim] - x[dim];

            if (d * d > minDistance)
            {
                if (node.x[dim] > x[dim])
                    CheckSubtree(node.Left, x);
                else
                    CheckSubtree(node.Right, x);
            }
            else
            {
                CheckSubtree(node.Left, x);
                CheckSubtree(node.Right, x);
            }
        }

        public void SetBoundingCube(Node node, double[] x)
        {
            if (node == null)
                return;
            int d = 0;
            double dx;
            for (int k = 0; k < 2; k++)
            {
                dx = node.x[k] - x[k];
                if (dx > 0)
                {
                    dx *= dx;
                    if (!maxBoundary[k])
                    {
                        if (dx > xMax[k])
                            xMax[k] = dx;
                        if (xMax[k] > minDistance)
                        {
                            maxBoundary[k] = true;
                            nodeBoundary++;
                        }
                    }
                }
                else
                {
                    dx *= dx;
                    if (!minBoundary[k])
                    {
                        if (dx > xMin[k])
                            xMin[k] = dx;
                        if (xMin[k] > minDistance)
                        {
                            minBoundary[k] = true;
                            nodeBoundary++;
                        }
                    }
                }
                d += Convert.ToInt32(dx);
                if (d > minDistance)
                    return;

            }

            if (d < minDistance)
            {
                minDistance = d;
                nearestNeighbour = node;
            }
        }

        public Node SearchParent(Node parent, double[] x)
        {
            for (int k = 0; k < 2; k++)
            {
                xMin[k] = xMax[k] = 0;
                maxBoundary[k] = minBoundary[k] = false; //
            }
            nodeBoundary = 0;

            Node search_root = parent;
            while (parent != null && (nodeBoundary != 2 * 2))
            {
                CheckSubtree(parent, x);
                search_root = parent;
                parent = parent.Parent;
            }

            return search_root;
        }

        public void Uncheck()
        {
            for (int n = 0; n < seenNode; n++)
                seenNodes[n].seenNode = false;
        }

    }

}