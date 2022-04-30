using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.GS
{
    //lib utility
    public sealed  class IGSLib : IGSContext 
    {
        private IGSContext m_context;
        private static IGSLib sm_instance;

        private IGSLib()
        {
        }

        private static IGSLib Instance
        {
            get
            {
                return sm_instance;
            }
        }
        static IGSLib()
        {
            sm_instance = new IGSLib();
        }
        public static void Init(IGSContext context)
        {
            sm_instance.m_context = context;
        }

        public IGSDbContext DbContext
        {
            get { return this.m_context.DbContext; }
        }

        internal static string CreateTableQuery(string name, GSDBColumnInfo[] togoDBColumnInfo, string description)
        {
            return sm_instance.m_context.DbContext.CreateTableQuery(name, togoDBColumnInfo, description);
        }


        public IGSDataQueryResult SelectAll(Type t, Dictionary<string, object> andCondition)
        {
             return sm_instance.m_context.SelectAll (t, andCondition );
        }
    }
}
