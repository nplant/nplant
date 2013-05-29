using NPlant.Generation;

namespace NPlant.MetaModel.ClassDiagraming
{
    public class NullIAggregationDescriptor : IAggregationDescriptor
    {
        public NullIAggregationDescriptor()
        {
            this.Key = null;
            this.LeftName = null;
            this.RightName = null;
            this.Label = null;
            this.Verb = null;
            this.Noun = null;
        }

        public string Key { get; private set; }
        public string LeftName { get; private set; }
        public string RightName { get; private set; }
        public string Verb { get; private set; }
        public string Noun { get; private set; }
        public string Label { get; private set; }
        public IBuilder CreateClassBuilder()
        {
            return new NullBuilder();
        }
    }
}