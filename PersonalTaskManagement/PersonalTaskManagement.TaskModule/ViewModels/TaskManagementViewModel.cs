using Prism.Commands;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;
using System.Collections.ObjectModel;
using System.Windows.Input;
using PersonalTaskManagement.Model;

namespace PersonalTaskManagement.TaskModule
{
    internal class TaskManagementViewModel : BindableBase
    {
        /// <summary>
        /// 获取或设置通知请求
        /// <para>关联属性: NotificationRequest</para>
        /// </summary>
        private InteractionRequest<Confirmation> _notificationRequest;

        /// <summary>
        /// 获取或设置通知请求
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

        /// <summary>
        /// 集合对象
        /// <para>关联属性: Tasks</para>
        /// </summary>
        private ObservableCollection<TaskModel> _Tasks;

        /// <summary>
        /// 集合对象
        /// </summary>
        public ObservableCollection<TaskModel> Tasks
        {
            get
            {
                if (_Tasks == null) _Tasks = new ObservableCollection<TaskModel>();
                return _Tasks;
            }
            set
            {
                _Tasks = value;
                OnPropertyChanged("Tasks");
            }
        }

        #region 添加 任务

        /// <summary>
        /// 添加任务 属性字段 [命令]
        /// <para>关联属性:AddTaskCommand</para>
        /// </summary>
        private ICommand _addTaskCommand;

        /// <summary>
        /// 添加任务 [命令]
        /// </summary>
        public ICommand AddTaskCommand
        {
            get
            {
                if (_addTaskCommand == null) _addTaskCommand = new DelegateCommand(AddTaskExecute, AddTaskCanExecute);
                return _addTaskCommand;
            }
            set { _addTaskCommand = value; }
        }

        /// <summary>
        /// 添加任务 [命令执行方法]
        /// </summary>
        public void AddTaskExecute()
        {
            Confirmation etn = new Confirmation();
            etn.Title = "新增任务";
            etn.Content = new TaskModel() { Name = "称名" };
            this.NotificationRequest.Raise(etn, (t) =>
            {
                if (t.Confirmed)
                {
                    Tasks.Add(t.Content as TaskModel);
                }
            });
        }

        /// <summary>
        ///  添加任务 [命令校验方法]
        /// </summary>
        public bool AddTaskCanExecute()
        {
            return true;
        }

        #endregion 添加

        #region 编辑 任务

        /// <summary>
        /// 编辑任务 命令字段
        /// <para>关联属性:EditCommand</para>
        /// </summary>
        private ICommand _editCommand;

        /// <summary>
        /// 编辑任务 [命令]
        /// </summary>
        public ICommand EditCommand
        {
            get
            {
                if (_editCommand == null) _editCommand = new DelegateCommand<TaskModel>(EditExecute, EditCanExecute);
                return _editCommand;
            }
            set { _editCommand = value; }
        }

        /// <summary>
        /// 编辑任务 [命令执行方法]
        /// </summary>
        public void EditExecute(TaskModel editObject)
        {
            Confirmation etn = new Confirmation();
            int d = Tasks.IndexOf(editObject);
            etn.Title = "编辑任务";
            etn.Content = editObject.Clone();
            this.NotificationRequest.Raise(etn, (t) =>
            {
                if (t.Confirmed)
                {
                    Tasks[d] = t.Content as TaskModel;
                }
            });
        }

        /// <summary>
        ///  编辑任务 [命令校验方法]
        /// </summary>
        public bool EditCanExecute(TaskModel editObject)
        {
            return true;
        }

        #endregion 编辑任务

        #region 删除任务

        /// <summary>
        /// 删除任务 字段
        /// <para>关联属性:DeleteCommand</para>
        /// </summary>
        private ICommand _DeleteCommand;

        /// <summary>
        /// 删除任务 [命令]
        /// </summary>
        public ICommand DeleteCommand
        {
            get
            {
                if (_DeleteCommand == null) _DeleteCommand = new DelegateCommand<TaskModel>(DeleteExecute, DeleteCanExecute);
                return _DeleteCommand;
            }
            set { _DeleteCommand = value; }
        }

        /// <summary>
        /// 删除任务 [命令执行方法]
        /// </summary>
        public void DeleteExecute(TaskModel editObject)
        {
            Tasks.Remove(editObject);
        }

        /// <summary>
        ///  删除任务 [命令校验方法]
        /// </summary>
        public bool DeleteCanExecute(TaskModel editObject)
        {
            return true;
        }

        #endregion 删除任务

        #region 刷新任务
        /// <summary>
        /// 刷新任务 属性字段 [命令]
        /// <para>关联属性:RefreshCommand</para>
        /// </summary>
        private ICommand _RefreshCommand;
        /// <summary>
        /// 刷新任务 [命令]
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
        /// 刷新任务 [命令执行方法]
        /// </summary>
        public void RefreshExecute()
        {

        }
        /// <summary>
        ///  刷新任务 [命令校验方法]
        /// </summary>
        public bool RefreshCanExecute()
        {
            return true;
        }
        #endregion

        #region 查询 任务
        /// <summary>
        /// 查询 任务 属性字段 [命令]
        /// <para>关联属性:FindCommand</para>
        /// </summary>
        private ICommand _findCommand;
        /// <summary>
        /// 查询 任务 [命令]
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
        /// 查询 任务 [命令执行方法]
        /// </summary>
        public void FindExecute()
        {

        }
        /// <summary>
        ///  查询 任务 [命令校验方法]
        /// </summary>
        public bool FindCanExecute()
        {
            return true;
        }
        #endregion

    }
}