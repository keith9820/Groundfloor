using System;
using System.Web;

namespace Groundfloor.Web
{
    public class DebugModule : IHttpModule
    {
        /// <summary>
        /// You will need to configure this module in the Web.config file of your
        /// web and register it with IIS before being able to use it. For more information
        /// see the following link: http://go.microsoft.com/?linkid=8101007
        /// </summary>
        #region IHttpModule Members

        public void Dispose()
        {
            //clean-up code here.
        }

        public void Init(HttpApplication context)
        {
            // Below is an example of how you can handle LogRequest event and provide 
            // custom logging implementation for it
            context.LogRequest += new EventHandler(OnLogRequest);

            context.PreSendRequestContent += OnPreSendRequestContent;
        }

        void OnPreSendRequestContent(object sender, EventArgs e)
        {
            if (!HttpContext.Current.IsDebuggingEnabled)
                return;

            var box = @"<section id='sizeBox' class='auto-margin' style='background:#888;border-bottom:1px solid #999;'>
            <div class='box'>
                <strong class='tagVersion'>tags/qa/2.0.35</strong>
                <span>Viewport Width: 1793</span>
            </div>
        </section>";

            HttpContext.Current.Response.Write(box);
        }

        #endregion

        public void OnLogRequest(Object source, EventArgs e)
        {
            //custom logging logic can go here
        }
    }
}
