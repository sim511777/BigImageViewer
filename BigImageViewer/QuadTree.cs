using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigImageViewer {
    public class QuadTree {
        public QuadTreeNode root;
        
        private QuadTree() {}
        public static QuadTree Generate(float minX, float minY, float maxX, float maxY, float minPitch) {
            float x1 = minX;
            float y1 = minY;
            float xSize = maxX-minX;
            float ySize = maxY-minY;
            float rootSize = xSize > ySize ? xSize : ySize;
            float x2 = x1 + rootSize;
            float y2 = y1 + rootSize;
            QuadTree tree = new QuadTree();
            tree.root = QuadTreeNode.Generate(x1, y1, x2, y2, minX, minY, maxX, maxY, minPitch);
            return tree;
        }

        public void AddHoles(Hole[] holes) {
            for (int i=0; i<holes.Length; i++) {
                var hole = holes[i];
                root.AddHole(hole);
            }
        }
    }

    public class QuadTreeNode {
        public float x1;
        public float y1;
        public float x2;
        public float y2;
        public Hole holeFront;
        public List<Hole> holes;    // null이 아니면 leaf
        public QuadTreeNode childLT;
        public QuadTreeNode childRT;
        public QuadTreeNode childLB;
        public QuadTreeNode childRB;

        private QuadTreeNode(float x1, float y1, float x2, float y2) {
            this.x1 = x1;
            this.y1 = y1;
            this.x2 = x2;
            this.y2 = y2;
        }

        public static QuadTreeNode Generate(float x1, float y1, float x2, float y2, float minX, float minY, float maxX, float maxY, float minPitch) {
            if (x1 > maxX || x2 < minX || y1 > maxY || y2 < minY)
                return null;

            QuadTreeNode node = new QuadTreeNode(x1, y1, x2, y2);
            if (x2-x1 <= minPitch) {
                node.holes = new List<Hole>();
                return node;
            }

            float xMid = (x1 + x2) * 0.5f;
            float yMid = (y1 + y2) * 0.5f;
            node.childLT = Generate(x1, y1, xMid, yMid, minX, minY, maxX, maxY, minPitch);
            node.childRT = Generate(xMid, y1, x2, yMid, minX, minY, maxX, maxY, minPitch);
            node.childLB = Generate(x1, yMid, xMid, y2, minX, minY, maxX, maxY, minPitch);
            node.childRB = Generate(xMid, yMid, x2, y2, minX, minY, maxX, maxY, minPitch);
            return node;
        }

        internal void AddHole(Hole hole) {
            // 대표 홀 처리
            // 마지막 리프이면 리스트에 Add하고 리턴
            // 아니면 홀의 좌표로 하나의 차일드에 Add
            if (holeFront == null) {
                holeFront = hole;
            } else {
                if (hole.x < holeFront.x && hole.y < holeFront.y)
                    holeFront = hole;
            }
            if (holes != null) {
                holes.Add(hole);
                    return;
            }
            float xMid = (x1 + x2) * 0.5f;
            float yMid = (y1 + y2) * 0.5f;
            if (hole.y <= yMid) {
                if (hole.x < xMid) {
                    childLT.AddHole(hole);
                } else {
                    childRT.AddHole(hole);
                }
            } else {
                if (hole.x < xMid) {
                    childLB.AddHole(hole);
                } else {
                    childRB.AddHole(hole);
                }
            }
        }
    }
}
