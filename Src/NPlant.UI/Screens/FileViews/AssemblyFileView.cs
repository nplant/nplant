namespace NPlant.UI
{
    public partial class AssemblyFileView : FileView, IAssemblyFileView
    {
        private readonly AssemblyFileViewController _controller;

        public AssemblyFileView()
        {
            InitializeComponent();
            _controller = new AssemblyFileViewController(this);
        }

        protected override void OnLoad(System.EventArgs e)
        {
            base.OnLoad(e);
            _controller.Start();
        }
    }
}
