using PersonalTaskManagement.Model;
using Prism.Commands;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace PersonalTaskManagement.TagModule
{
    internal class TagManagementViewModel : BindableBase
    {
        /// <summary>
        /// 获取或设置通知请求 属性字段
        /// <para>关联属性: NotificationRequest</para>
        /// </summary>
        private InteractionRequest<Confirmation> _notificationRequest;

        /// <summary>
        /// 通知集合
        /// </summary>
        public InteractionRequest<Confirmation> NotificationRequest
        {
            get
            {
                if (_notificationRequest == null) _notificationRequest = new InteractionRequest<Confirmation>();
                return _notificationRequest;
            }
            set
            {
                _notificationRequest = value;
                OnPropertyChanged("NotificationRequest");
            }
        }

        #region 添加

        /// <summary>
        /// 添加 属性字段 [命令]
        /// <para>关联属性:AddTagCommand</para>
        /// </summary>
        private ICommand _addTagCommand;

        /// <summary>
        /// 添加 [命令]
        /// </summary>
        public ICommand AddTagCommand
        {
            get
            {
                if (_addTagCommand == null) _addTagCommand = new DelegateCommand(AddTagExecute, AddTagCanExecute);
                return _addTagCommand;
            }
            set { _addTagCommand = value; }
        }

        /// <summary>
        /// 添加 [命令执行方法]
        /// </summary>
        public void AddTagExecute()
        {
            Confirmation etn = new Confirmation();
            etn.Title = "新增标签";
            etn.Content = new TagModel() { Name = "称名" };
            this.NotificationRequest.Raise(etn, (t) =>
            {
                if (t.Confirmed)
                {
                    Tags.Add(t.Content as TagModel);
                }
            });
        }

        /// <summary>
        ///  添加 [命令校验方法]
        /// </summary>
        public bool AddTagCanExecute()
        {
            return true;
        }

        #endregion 添加

        #region 编辑标签

        /// <summary>
        /// 编辑标签 属性字段 [命令]
        /// <para>关联属性:EditCommand</para>
        /// </summary>
        private ICommand _editCommand;

        /// <summary>
        /// 编辑标签 [命令]
        /// </summary>
        public ICommand EditCommand
        {
            get
            {
                if (_editCommand == null) _editCommand = new DelegateCommand<TagModel>(EditExecute, EditCanExecute);
                return _editCommand;
            }
            set { _editCommand = value; }
        }

        /// <summary>
        /// 编辑标签 [命令执行方法]
        /// </summary>
        public void EditExecute(TagModel editObject)
        {
            Confirmation etn = new Confirmation();
            int d = Tags.IndexOf(editObject);
            etn.Title = "编辑标签";
            etn.Content = editObject.Clone();
            this.NotificationRequest.Raise(etn, (t) =>
            {
                if (t.Confirmed)
                {
                    Tags[d] = t.Content as TagModel;
                }
            });
        }

        /// <summary>
        ///  编辑标签 [命令校验方法]
        /// </summary>
        public bool EditCanExecute(TagModel editObject)
        {
            return true;
        }

        #endregion 编辑标签

        #region 删除标签

        /// <summary>
        /// 删除标签 属性字段 [命令]
        /// <para>关联属性:DeleteCommand</para>
        /// </summary>
        private ICommand _DeleteCommand;

        /// <summary>
        /// 删除标签 [命令]
        /// </summary>
        public ICommand DeleteCommand
        {
            get
            {
                if (_DeleteCommand == null) _DeleteCommand = new DelegateCommand<TagModel>(DeleteExecute, DeleteCanExecute);
                return _DeleteCommand;
            }
            set { _DeleteCommand = value; }
        }

        /// <summary>
        /// 删除标签 [命令执行方法]
        /// </summary>
        public void DeleteExecute(TagModel editObject)
        {
            Tags.Remove(editObject);
        }

        /// <summary>
        ///  删除标签 [命令校验方法]
        /// </summary>
        public bool DeleteCanExecute(TagModel editObject)
        {
            return true;
        }

        #endregion 删除标签

        #region 刷新[Tag]
        /// <summary>
        /// 刷新[Tag] 属性字段 [命令]
        /// <para>关联属性:RefreshCommand</para>
        /// </summary>
        private ICommand _RefreshCommand;
        /// <summary>
        /// 刷新[Tag] [命令]
        /// </summary>
        public ICommand RefreshCommand
        {
            get
            {
                if (_RefreshCommand == null) _RefreshCommand = new DelegateCommand(RefreshExecute, RefreshCanExecute);
                return _RefreshCommand;
            }
            set { _RefreshCommand = value; }
        }

        /// <summary>
        /// 刷新[Tag] [命令执行方法]
        /// </summary>
        public void RefreshExecute()
        {

        }
        /// <summary>
        ///  刷新[Tag] [命令校验方法]
        /// </summary>
        public bool RefreshCanExecute()
        {
            return true;
        }
        #endregion

        #region 查询 [Tag]
        /// <summary>
        /// 查询 [Tag] 属性字段 [命令]
        /// <para>关联属性:FindCommand</para>
        /// </summary>
        private ICommand _findCommand;
        /// <summary>
        /// 查询 [Tag] [命令]
        /// </summary>
        public ICommand FindCommand
        {
            get
            {
                if (_findCommand == null) _findCommand = new DelegateCommand(FindExecute, FindCanExecute);
                return _findCommand;
            }
            set { _findCommand = value; }
        }

        /// <summary>
        /// 查询 [Tag] [命令执行方法]
        /// </summary>
        public void FindExecute()
        {

        }
        /// <summary>
        ///  查询 [Tag] [命令校验方法]
        /// </summary>
        public bool FindCanExecute()
        {
            return true;
        }
        #endregion

        /// <summary>
        /// 集合对象 属性字段
        /// <para>关联属性: Tags</para>
        /// </summary>
        private ObservableCollection<TagModel> _tags;

        /// <summary>
        /// 集合对象
        /// </summary>
        public ObservableCollection<TagModel> Tags
        {
            get
            {
                if (_tags == null) _tags = new ObservableCollection<TagModel>();
                return _tags;
            }
            set
            {
                _tags = value;
                OnPropertyChanged("Tags");
            }
        }
    }
}