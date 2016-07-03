using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Data;
using HMSDL;
using HMSOM;

namespace HMSBL
{
    [Serializable]
    public class NotifyQueueBL
    {

        #region Declaration
        private string connectionString;
        NotifyQueue _NotifyQueue;
        public NotifyQueue Data
        {
            get { return _NotifyQueue; }
            set { _NotifyQueue = value; }
        }
        public bool IsNew
        {
            get { return (_NotifyQueue.Id <= 0 || _NotifyQueue.Id == null); }
        }
        #endregion

        #region Constructor
        public NotifyQueueBL(string conString)
        {
            connectionString = conString;
        }
        #endregion

        #region Main Methods
        private NotifyQueueDL CreateDL()
        {
            return new NotifyQueueDL(connectionString);
        }
        public void New()
        {
            _NotifyQueue = new NotifyQueue();
        }
        public void Load(int Id)
        {
            var NotifyQueueObj = this.CreateDL();
            _NotifyQueue = Id <= 0 ? NotifyQueueObj.Load(-1) : NotifyQueueObj.Load(Id);
        }
        public DataTable LoadAllNotifyQueue()
        {
            var NotifyQueueDLObj = CreateDL();
            return NotifyQueueDLObj.LoadAllNotifyQueue();
        }
        public bool Update()
        {
            var NotifyQueueDLObj = CreateDL();
            return NotifyQueueDLObj.Update(this.Data);
        }
        public bool Delete(int Id)
        {
            var NotifyQueueDLObj = CreateDL();
            return NotifyQueueDLObj.Delete(Id);
        }
        #endregion
    }
}
