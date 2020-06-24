using Autofac;
using AutoMapper;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Serilog;
using Serilog.Sinks.RollingFileAlternate;
using Softplan.Desafio.Api1;
using Softplan.Desafio.Infra.CrossCutting.IoC;
using Softplan.Desafio.Response;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;

namespace Softplan.Desafio.Api1
{
    public class Startup: StartupBase
    {
        public Startup(IConfiguration configuration): base(configuration)
        {
            this.ProjectTitleName = "Softplan.Desafio.Api1";
        }

        public override void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new ApplicationModule());
            builder.RegisterType<Presenter>();
        }
    }
}