using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigImageViewer {
    public class Hole {
        public float x;
        public float y;
        public float w;
        public float h;
        public int idxX;
        public int idxY;
        public int fwd;
        public Hole(float x, float y, float w, float h, int idxX, int idxY, int fwd) {
            this.x = x;
            this.y = y;
            this.w = w;
            this.h = h;
            this.idxX = idxX;
            this.idxY = idxY;
            this.fwd = fwd;
        }
        public override string ToString() {
            return $"Hole(x:{x},y:{y},dx:{w},dy:{h},idx:{idxX},idy:{idxY},fwd:{fwd})";
        }
    }
}
