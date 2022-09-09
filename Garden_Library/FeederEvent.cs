using System;
using System.Collections.Generic;
using System.Text;

namespace Garden_Library
{
    public class FeederEventArgs
    {
        Feeder feeder;
        public Feeder Feeder { get { return feeder; } }

        public FeederEventArgs(Feeder feeder)
        {
            this.feeder = feeder;
        }
    }


    public delegate void FeederHendler(FeederEventArgs e);
}
