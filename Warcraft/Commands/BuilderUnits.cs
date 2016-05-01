using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warcraft.Units.Humans;

namespace Warcraft.Commands
{
    class BuilderUnits : ICommand
    {
        public bool go;
        public bool completed;
        public bool remove;

        public int elapsed;
        public int total;

        public Util.Units type;

        public BuilderUnits(Util.Units type, int total)
        {
            this.total = total;
            this.type = type;
        }

        public void execute()
        {
            go = true;
            completed = false;
            remove = false;
        }

        public void Update()
        {
            if (go)
            {
                elapsed++;
                if (elapsed > total)
                {
                    completed = true;
                    go = false;

                    elapsed = 0;
                }
            }
        }
    }
}
