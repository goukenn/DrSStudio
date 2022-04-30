
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.GS
{
    using IGK.GS.DataTable;

    public static class GSOperationExtension
    {
        /// <summary>
        /// Convert int to interface db
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <returns></returns>
        public static T GSDBSelect<T>(this int item)
        {
            return GSDB.SelectFirstRowInstance<T>(item.ToIdentifier());
        }
        public static bool IsUserAuthorised(this ITbGSUsers s, string actionName)
        {
            if (s == null)
                return false;
            if (s.clLevel == -1)
                return true;//administrator have all access
            Type v_tAu = typeof(ITbGSAuthorisations);
            IGSDataQueryResult r = GSDB.SelectAll(typeof(ITbGSAuthorisations), new Dictionary<string, object>() { 
                {"clName",actionName }
            });
            bool v_r = true;
            if (GSDB.IsSingle(r))
            {
                //get autorisation id
                string v_authid = r.GetRowAt(0)[GSConstant.CL_ID];
                //find group that user is member of
                var v_usergroup = GSDB.SelectAll(typeof(ITbGSUserGroups), new Dictionary<string, object>() { 
                    {"clUserId", s.clId}
                });
                foreach (IGSDataRow item in v_usergroup.Rows)
                {
                    var q = GSDB.SelectAll(typeof(ITbGSGroupAutorisations), new Dictionary<string, object>() { 
                    {"clGroupId", item["clGroupId"]},
                    {"clAuthId", v_authid}
                });

                    if (GSDB.IsSingle(q))
                    {
                        v_r = v_r && (q.GetRowAt(0).GetValue<int>("clAutorisation") == 1);
                    }
                }
                return !v_r;
            }
            else
            {

                ITbGSAuthorisations p = GSDB.CreateInterfaceInstance<ITbGSAuthorisations>();
                p.clName = actionName;
                p.Insert();
            }
            return false;
        }
        /// <summary>
        /// Get if current user is autorized to do an action
        /// </summary>
        /// <param name="ActionName">Action that is requested to do</param>
        /// <returns></returns>
        public static bool IsUserAutorised(string ActionName)
        {
            return IsUserAutorised(
                global::System.Convert.ToInt32(GSSystem.User[GSConstant.CL_ID]),
                ActionName);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="uid">user id</param>
        /// <param name="actionName">autorisation</param>
        /// <returns></returns>
        public static bool IsUserAutorised(int uid, string actionName)
        {
            ITbGSUsers user = GSDB.SelectFirstRowInstance<ITbGSUsers>(uid.ToIdentifier(GSConstant.CL_ID));
            if (user != null)
            {
                return IsUserAuthorised(user, actionName);
            }
            return false;

        }
    }
}
