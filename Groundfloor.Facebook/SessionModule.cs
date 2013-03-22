using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Groundfloor.Facebook;
using Groundfloor.Facebook.Config;
using System.Diagnostics;

namespace Groundfloor.Web.HttpModule
{
    public class FBSessionModule: IHttpModule
    {
        public void Init(HttpApplication context)
        {
            context.PostAcquireRequestState += new EventHandler(context_PostAcquireRequestState);
            context.BeginRequest += new EventHandler(context_BeginRequest);

            //this doesn't work under medium trust
            //AppDomain.CurrentDomain.FirstChanceException += new EventHandler<System.Runtime.ExceptionServices.FirstChanceExceptionEventArgs>(CurrentDomain_FirstChanceException);
        }

        void CurrentDomain_FirstChanceException(object sender, System.Runtime.ExceptionServices.FirstChanceExceptionEventArgs e)
        {
            Debug.WriteLine(e.Exception.Message);
        }

        void context_BeginRequest(object sender, EventArgs e)
        {
            var context = (sender as HttpApplication);
            var req = context.Request;
            Debug.WriteLine("begin request: {0}",req.Url);
        }

        void context_PostAcquireRequestState(object sender, EventArgs e)
        {
            var ctx = HttpContext.Current;

            Debug.WriteLine("PostAcquireRequestState: {0}", ctx.Handler);

            if (ctx.Session == null)
            {
                Debug.WriteLine("Session state is not available.");
                return;
            }

            var factory = new FacebookFactory();
            FacebookInstance fb = (FacebookInstance)ctx.Session["FBInstance"];
            string signedRequest = ctx.Request.Params["signed_request"];

           //whenever there is a new signed_request, refresh the FB object 
            if (signedRequest.HasValue() || fb == null)
            {
                var key = ctx.Request.Params["app"];
                var elem = FacebookConfigManager.GetInstance(key);
                fb = factory.GetInstance(elem, ctx.Request.Params);
            }
            ctx.Session["FBInstance"] = fb;
        }

        public void Dispose() { }
    }
}