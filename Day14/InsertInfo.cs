using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day14
{
    public readonly struct InsertInfo
    {
        public string Rule { get; }

        public String Insert { get; }

        public InsertInfo(in String rule, in String insert)
        {
            this.Rule = rule;
            this.Insert = insert;
        }
    }
}
