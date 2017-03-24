using Prism.Commands;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;
using System;
using System.Windows.Input;

/// <summary>
/// TagModule 的名称空间
/// </summary>
namespace PersonalTaskManagement.TagModule
{
    /// <summary>
    /// EditTagViewModel类
    /// </summary>
    public class EditTagViewModel : BindableBase, IInteractionRequestAware
    {
        #region 确认

        /// <summary>
        /// 确认 属性字段 [命令]
        /// <para>关联属性:ConfirmCommand</para>
        /// </summary>
        private ICommand _confirmCommand;

        /// <summary>
        /// 确认 [命令]
        /// </summary>
        /// <value>The confirm command.</value>
        /// <remarks>无</remarks>
        public ICommand ConfirmCommand
        {
            get
            {
                if (_confirmCommand == null) _confirmCommand = new DelegateCommand(ConfirmExecute, ConfirmCanExecute);
                return _confirmCommand;
            }
            set { _confirmCommand = value; }
        }

        /// <summary>
        /// 确认 [命令执行方法]
        /// </summary>
        /// <remarks>无</remarks>
        public void ConfirmExecute()
        {
            (Notification as Confirmation).Confirmed = true;
            this.FinishInteraction();
        }

        /// <summary>
        /// 确认 [命令校验方法]
        /// </summary>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        /// <remarks>无</remarks>
        public bool ConfirmCanExecute()
        {
            return true;
        }

        #endregion 确认

        #region 取消

        /// <summary>
        /// 取消 属性字段 [命令]
        /// <para>关联属性:CancelCommand</para>
        /// </summary>
        private ICommand _cancelCommand;

        /// <summary>
        /// 取消 [命令]
        /// </summary>
        /// <value>The cancel command.</value>
        /// <remarks>无</remarks>
        public ICommand CancelCommand
        {
            get
            {
                if (_cancelCommand == null) _cancelCommand = new DelegateCommand(CancelExecute, CancelCanExecute);
                return _cancelCommand;
            }
            set { _cancelCommand = value; }
        }

        /// <summary>
        /// 取消 [命令执行方法]
        /// </summary>
        /// <remarks>无</remarks>
        public void CancelExecute()
        {
            (Notification as Confirmation).Confirmed = false;
            this.FinishInteraction();
        }

        /// <summary>
        /// 取消 [命令校验方法]
        /// </summary>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        /// <remarks>无</remarks>
        public bool CancelCanExecute()
        {
            return true;
        }

        #endregion 取消

        public Action FinishInteraction { get; set; }

        /// <summary>
        /// 通知对象 属性字段
        /// <para>关联属性: Notification</para>
        /// </summary>
        private INotification _notification;

        /// <summary>
        /// 通知对象
        /// </summary>
        public INotification Notification
        {
            get
            {
                if (_notification == null) _notification = new Confirmation();
                return _notification;
            }
            set
            {
                _notification = value;
                OnPropertyChanged("Notification");
            }
        }
    }
}