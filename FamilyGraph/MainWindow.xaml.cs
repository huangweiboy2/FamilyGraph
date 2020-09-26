using System.Windows;
using FamilyGraph.Internal;
using FamilyGraph.ViewModel;

namespace FamilyGraph
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainViewModel _viewModel;

        public MainWindow()
        {
            InitializeComponent();
            InitViewModel();
            InitData();
        }

        private void InitData()
        {
            _viewModel.FamilyTreeNodes.Add(GenerateDefaultTree());
        }

        private void InitViewModel()
        {
            _viewModel = new MainViewModel {Owner = this};
            InitCommand();
            DataContext = _viewModel;
        }

        private void InitCommand()
        {
        }

        private FamilyTreeNode GenerateDefaultTree()
        {
            var son1 = new FamilyTreeNode
            {
                Name = "儿子1",
                Type = Gender.Male
            };
            var son2 = new FamilyTreeNode
            {
                Name = "儿子2",
                SpouseName = "儿媳妇2",
                Type = Gender.Male
            };
            var daughter = new FamilyTreeNode
            {
                Name = "女儿",
                Type = Gender.Female
            };

            var me = new FamilyTreeNode
            {
                Name = "我",
                SpouseName = "老婆",
                Type = Gender.Male
            };
            me.AddChild(son1);
            me.AddChild(son2);
            me.AddChild(daughter);

            var brother = new FamilyTreeNode
            {
                Name = "哥哥",
                SpouseName = "嫂子",
                Type = Gender.Male
            };

            var father = new FamilyTreeNode
            {
                Name = "爸爸",
                SpouseName = "妈妈",
                Type = Gender.Male
            };
            father.AddChild(me);
            father.AddChild(brother);

            var grandpa = new FamilyTreeNode
            {
                Name = "爷爷",
                SpouseName = "奶奶",
                Type = Gender.Male
            };
            grandpa.AddChild(father);

            return grandpa;
        }
    }
}