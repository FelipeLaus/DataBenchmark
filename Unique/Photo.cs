using System;
using System.Collections.Generic;
using System.Text;

namespace DataBenchmark.Unique
{
    public class Photo
    {
        public int Id { get; set; }
        public string Name { get; set; }


        public override int GetHashCode() => Id.GetHashCode();
        public override bool Equals(object obj) => obj is Photo f && f.Id == Id;
    }
}
