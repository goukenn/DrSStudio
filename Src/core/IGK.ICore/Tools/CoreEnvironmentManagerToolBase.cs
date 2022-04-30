

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreEnvironmentManagerToolBase.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:CoreEnvironmentManagerToolBase.cs
*/
using System;
namespace IGK.ICore.Tools 
{
	using IGK.ICore;using IGK.ICore.Settings ;
	/// <summary>
	/// Core environment manager tool base.
	/// </summary>
	public abstract class CoreEnvironmentManagerToolBase : CoreToolBase, ICoreWorkingEnvironmentManagerTool 
	{
		public  const string ENV_NAMES = "LayoutEnvironments";
        public const string ENV_CONFIGS = "LayoutConfigs";
		string m_environmentName;
		protected  CoreEnvironmentManagerToolBase ()
		{
            this.m_setting = new CoreEnvironmentSettingBase();
		}
		CoreEnvironmentSettingBase m_setting;
		/// <summary>
		/// Gets the environment setting base.
		/// </summary>
		/// <value>
		/// The setting.
		/// </value>
		public CoreEnvironmentSettingBase Setting{
			get{
				return this.m_setting ;
			}
		}
		#region ICoreWorkingEnvironmentManagerTool implementation
		public event EventHandler EnvironmentChanged;
		public string EnvironmentName {
			get {
				return this.m_environmentName ;
			}
			set {
				if (this.m_environmentName !=null)
				{
					this.m_environmentName = value;
					OnEnvironmentChanged(EventArgs.Empty);
				}
			}
		}
		#endregion
		public void OnEnvironmentChanged(EventArgs e)
		{
			if (this.EnvironmentChanged !=null)
				this.EnvironmentChanged (this, e);
		}
	}
}

