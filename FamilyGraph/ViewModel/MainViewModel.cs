using System;
using System.Collections.ObjectModel;
using System.Windows;
using FamilyGraph.Internal;
using FamilyGraph.Undoable;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using Microsoft.Win32;

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
        private ActionHistory _actionHistory = new ActionHistory();
        private bool _isSaved = false;

        #region Properties

        private string _filePath = String.Empty;

        public string FilePath
        {
            get => _filePath;
            set
            {
                _filePath = value;
                RaisePropertyChanged();
            }
        }

        public ObservableCollection<FamilyTreeNode> FamilyTreeNodes
        {
            get => _familyTreeNodes;
            set
            {
                _familyTreeNodes = value;
                RaisePropertyChanged();
            }
        }

        #endregion

        #region Commands

        public RelayCommand<FamilyTreeNode> AddEmptyChildCommand { get; set; }
        public RelayCommand<FamilyTreeNode> RemoveCurrentNodeCommand { get; set; }
        public RelayCommand NewFileCommand { get; set; }
        public RelayCommand OpenFileCommand { get; set; }
        public RelayCommand SaveFileCommand { get; set; }
        public RelayCommand SaveFileAsCommand { get; set; }
        public RelayCommand PrintCommand { get; set; }
        public RelayCommand ExitCommand { get; set; }
        public RelayCommand UndoCommand { get; set; }
        public RelayCommand RedoCommand { get; set; }
        public RelayCommand ResetTreeCommand { get; set; }
        public RelayCommand PreviewGraphCommand { get; set; }
        public RelayCommand AboutUsCommand { get; set; }

        #endregion

        public MainViewModel()
        {
            // File Commands
            NewFileCommand = new RelayCommand(NewFile);
            OpenFileCommand = new RelayCommand(OpenFile);
            SaveFileCommand = new RelayCommand(SaveFile);
            SaveFileAsCommand = new RelayCommand(SaveFileAs);
            PrintCommand = new RelayCommand(Print);

            // Edit Commands
            UndoCommand = new RelayCommand(execute: Undo, () => _actionHistory.UndoCount > 0);
            RedoCommand = new RelayCommand(Redo, () => _actionHistory.RedoCount > 0);

            // Graph Command
            ResetTreeCommand = new RelayCommand(ResetTree);
            AddEmptyChildCommand =
                new RelayCommand<FamilyTreeNode>(AddChild, node => Owner?.TreeViewFamily?.SelectedItem != null);
            RemoveCurrentNodeCommand = new RelayCommand<FamilyTreeNode>(
                RemoveCurrentNode
                , node => (Owner?.TreeViewFamily?.SelectedItem as FamilyTreeNode)?.ParentNode != null);

            // Help Commands
            AboutUsCommand = new RelayCommand(AboutUs);

            // Others Commands
            ExitCommand = new RelayCommand(Exit);
        }


        #region File Operation

        public bool AskForSave()
        {
            if (_actionHistory.UndoCount > 0)
            {
                MessageBoxResult messageBoxResult =
                    MessageBox.Show(Owner, "当前族谱图未保存，是否先保存呢？", "新建", MessageBoxButton.YesNoCancel);
                switch (messageBoxResult)
                {
                    case MessageBoxResult.Yes:
                        SaveFileCommand?.Execute(null);
                        if (!_isSaved)
                            return false;
                        break;
                    case MessageBoxResult.No:
                        break;
                    case MessageBoxResult.Cancel:
                        return false;
                }
            }

            return true;
        }

        private string SelectSavePath()
        {
            var saveFileDialog = new SaveFileDialog
            {
                Filter = "家族图谱文件(*.fg)|*.fg"
            };
            var r = saveFileDialog.ShowDialog();
            if (r != null && r.Value)
            {
                return saveFileDialog.FileName;
            }

            return string.Empty;
        }


        private void NewFile()
        {
            if (!AskForSave())
                return;
            _actionHistory.Clear();
            FilePath = string.Empty;
            FamilyTreeNodes = new ObservableCollection<FamilyTreeNode>
            {
                new FamilyTreeNode
                {
                    Name = "祖宗",
                    Type = Gender.Male
                }
            };
        }

        private void OpenFile()
        {
            if (AskForSave())
            {
                var openFileDialog = new OpenFileDialog
                {
                    Multiselect = false,
                    Filter = "家族图谱文件(*.fg)|*.fg"
                };
                var r = openFileDialog.ShowDialog();
                if (r != null && r.Value)
                {
                    FilePath = openFileDialog.FileName;
                    MessageBox.Show(openFileDialog.FileName);
                }
            }
        }

        private void SaveFile()
        {
            if (FilePath == string.Empty)
            {
                var path = SelectSavePath();
                if (path != string.Empty)
                {
                    FilePath = path;
                    _isSaved = true;
                }
            }

            if (FilePath != string.Empty)
            {
                MessageBox.Show(FilePath);
            }
        }

        private void SaveFileAs()
        {
            var path = SelectSavePath();
            if (path != string.Empty)
            {
                FilePath = path;
                MessageBox.Show(FilePath);
            }
        }

        private void Print()
        {
            MessageBox.Show("正在快马加鞭开发中呢~");
        }

        #endregion

        #region Edit Operation

        private void Undo()
        {
            _actionHistory.Undo(1);
        }

        private void Redo()
        {
            _actionHistory.Redo(1);
        }

        private void ResetTree()
        {
            if (MessageBoxResult.Yes ==
                MessageBox.Show(Owner, "确定要重置图谱吗？这样之前的信息会丢失哦~", "提示", MessageBoxButton.YesNo))
            {
                _actionHistory.DoAction(new ResetTreeAction(this));
                // FamilyTreeNodes.Clear();
                // FamilyTreeNodes.Add(new FamilyTreeNode
                // {
                //     Name = "祖宗",
                //     Type = Gender.Male
                // });
            }
        }


        private void AddChild(FamilyTreeNode node)
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
                    _actionHistory.DoAction(new AddChildAction(node, editNodeWindow.Node));
                // node.Children.Add(editNodeWindow.Node);
            }
        }

        private void RemoveCurrentNode(FamilyTreeNode node)
        {
            if (node?.ParentNode != null)
            {
                _actionHistory.DoAction(new RemoveNodeAction(node));
            }
        }

        #endregion

        private void AboutUs()
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
        }

        private void Exit()
        {
            Owner.Close();
        }
    }
}