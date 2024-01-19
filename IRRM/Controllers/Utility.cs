using IRRM.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace IRRM.Controllers
{
    public class Utility
    {
        private IRRMEntities db = new IRRMEntities();
        public void setStatus(int IncidentID, int IncidentStatusID, int CreatedUserID)
        {
            String CreatedOn = Convert.ToDateTime(DateTime.UtcNow.AddHours(3)).ToString("yyyy/MM/dd HH:mm:ss");
            string sql = "INSERT INTO [Incident_Status_T] (IncidentID,IncidentStatusID,CreatedUserID,CreatedOn) values(" + IncidentID + "," + IncidentStatusID + "," + CreatedUserID + ",'" + CreatedOn + "');update incidents set incidentstatusid=" + IncidentStatusID + " where incidentid=" + IncidentID;
            db.Database.ExecuteSqlCommand(sql);
        }

        public void setStatusTask(int TaskID, int TaskStatusID, int CreatedUserID)
        {
            String CreatedOn = Convert.ToDateTime(DateTime.UtcNow.AddHours(3)).ToString("yyyy/MM/dd HH:mm:ss");
            string sql = "INSERT INTO [Task-Status_T] (TaskID,TaskStatusID,CreatedUserID,CreatedOn) values(" + TaskID + "," + TaskStatusID + "," + CreatedUserID + ",'" + CreatedOn + "');update Tasks set Taskstatusid=" + TaskStatusID + " where Taskid=" + TaskID;
            db.Database.ExecuteSqlCommand(sql);
        }



        public string Getregistryno(string eventtypeprefix)
        {
            
            string regval = db.Database.SqlQuery<string>("RegistryNo @prefix", new SqlParameter("prefix", eventtypeprefix)).SingleOrDefault();
            return regval;
        }

        // string regval = db.Database.SqlQuery<string>("update [EventType_SM] set registryno=registryno+1,updateddate=getdate() where prefix='" + eventtypeprefix + "';   ").SingleOrDefault();

    }
}