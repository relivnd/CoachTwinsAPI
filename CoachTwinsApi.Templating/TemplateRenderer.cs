using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;

namespace CoachTwinsApi.Templating
{
    public class TemplateRenderer
    {
        private readonly IRazorViewEngine _razorViewEngine;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ITempDataProvider _tempDataProvider;

        public TemplateRenderer(IRazorViewEngine razorViewEngine, IHttpContextAccessor httpContextAccessor, ITempDataProvider tempDataProvider)
        {
            _razorViewEngine = razorViewEngine;
            _httpContextAccessor = httpContextAccessor;
            _tempDataProvider = tempDataProvider;
        }

        public async Task<string> Render<T>(string template, T model)
        {
            if (_httpContextAccessor.HttpContext == null)
                throw new Exception("http context could not be found");
            
            var actionContext = new ActionContext(_httpContextAccessor.HttpContext, new RouteData(), new ActionDescriptor());
            var view = _razorViewEngine.GetView(null, $"View/{template}.cshtml", true);

            if (view.View == null)
                throw new Exception($"no template named {template} was found");

            var data = new ViewDataDictionary(new EmptyModelMetadataProvider(), new ModelStateDictionary())
            {
                Model = model
            };

            await using var stringWriter = new StringWriter();
            
            var context = new ViewContext(
                actionContext, 
                view.View, 
                data,
                new TempDataDictionary(_httpContextAccessor.HttpContext, _tempDataProvider), 
                stringWriter, 
                new HtmlHelperOptions()
            );

            await view.View.RenderAsync(context);

            return stringWriter.ToString();
        }
    }
}