﻿using System.Web.Mvc;

namespace MyCqrs1.Domain.Host.Web.Areas.Client
{
    public class ClientAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Client";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Client_default",
                "Client",
                new { controller = "Client", action = "Index" }
            );
        }
    }
}