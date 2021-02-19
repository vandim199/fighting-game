using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{
    class Button : AnimationSprite
    {
        public bool selected = false;
        public bool clicked = false;

        public Button(GameObject _menu, float _x, float _y, string _sprite, double _scale = 1, int _columns = 1, int _rows = 1) : base(_sprite, _columns, _rows)
        {
            SetXY(_x, _y);
            scale = (float)_scale;
            _menu.AddChild(this);
        }

        void Update()
        {
            if (Input.GetMouseButtonUp(0))
            {
                if (HitTestPoint(Input.mouseX, Input.mouseY))
                {
                    clicked = true;
                }
            }
            else
            {
                clicked = false;
            }

            if (selected)
            {
                SetFrame(1);
                if (Input.GetKeyDown(Key.E))
                {
                    clicked = true;
                }
            }
            else SetFrame(0);
        }
    }
}
