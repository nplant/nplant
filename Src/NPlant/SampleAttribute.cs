using System;

namespace NPlant
{
    public sealed class SampleAttribute : Attribute
    {
        public SampleAttribute()
        {
            
        }

        public SampleAttribute(string name, string description)
        {
            this.Name = name;
            this.Description = description;
        }

        public string Name { get; private set; }

        public string Description { get; private set; }
    }
}
