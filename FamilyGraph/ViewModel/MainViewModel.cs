using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using FamilyGraph.Controls;
using FamilyGraph.Internal;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;

namespace FamilyGraph.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        public MainWindow Owner;
        private ObservableCollection<FamilyTreeNode> _familyTreeNodes = new ObservableCollection<FamilyTreeNode>();
        private AboutWindow _aboutWindow = null;
        
        public ObservableCollection<FamilyTreeNode> FamilyTreeNodes
        {
            get => _familyTreeNodes;
            set
            {
                _familyTreeNodes = value;
                RaisePropertyChanged();
            }
        }

        public RelayCommand<FamilyTreeNode> AddEmptyChild { get; set; }
        public RelayCommand<FamilyTreeNode> RemoveCurrentNode { get; set; }
        public RelayCommand NewFile { get; set; }
        public RelayCommand OpenFile { get; set; }
        public RelayCommand SaveFile { get; set; }
        public RelayCommand SaveFileAs { get; set; }
        public RelayCommand Print { get; set; }
        public RelayCommand Exit { get; set; }
        public RelayCommand ResetTree { get; set; }
        public RelayCommand PreviewGraph { get; set; }
        public RelayCommand AboutUs { get; set; }

        public MainViewModel()
        {
            AddEmptyChild = new RelayCommand<FamilyTreeNode>(node =>
            {
                if (node != null)
                {
                    var newNode = new FamilyTreeNode
                    {
                        Name = "你说取个啥名好呢？",
                        Type = Gender.Male
                    };
                    var editNodeWindow = new EditNodeWindow(newNode);
                    var r = editNodeWindow.ShowDialog();
                    if (r != null && r.Value)
                        node.Children.Add(editNodeWindow.Node);
                }
            }, node => Owner?.TreeViewFamily?.SelectedItem != null);
            RemoveCurrentNode = new RelayCommand<FamilyTreeNode>(node => node.ParentNode?.Children.Remove(node)
                , node => (Owner?.TreeViewFamily?.SelectedItem as FamilyTreeNode)?.ParentNode != null);

            ResetTree = new RelayCommand(() =>
            {
                if (MessageBoxResult.Yes == MessageBox.Show(Owner,"确定要重置图谱吗？这样之前的信息会丢失哦~", "提示", MessageBoxButton.YesNo))
                {
                    FamilyTreeNodes.Clear();
                    FamilyTreeNodes.Add(new FamilyTreeNode
                    {
                        Name = "祖宗",
                        Type = Gender.Male
                    });
                }
            });
            
            AboutUs = new RelayCommand(()=>
            {
                if (_aboutWindow == null)
                {
                    _aboutWindow = new AboutWindow();
                    _aboutWindow.Show();
                    _aboutWindow = null;
                }
                else
                {
                    _aboutWindow.Activate();
                }
            });
        }
    }
}