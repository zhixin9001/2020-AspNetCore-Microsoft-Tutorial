using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _2_DI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DefaultController : ControllerBase
    {
        public OperationService OperationService { get; }
        public IOperationTransient TransientOperation { get; }
        public IOperationScoped ScopedOperation { get; }
        public IOperationSingleton SingletonOperation { get; }
        public IOperationSingletonInstance SingletonInstanceOperation { get; }


        public ISingletonTest singleton { get; }
        public ITransientTest transient { get; }
        public IScopedTest scoped { get; }

        public ScopeAndTransientTester scopeAndTransientTester { get; }

        public DefaultController(/*OperationService operationService,*/
        //IOperationTransient transientOperation,
        //IOperationScoped scopedOperation,
        //IOperationSingleton singletonOperation,
        //IOperationSingletonInstance singletonInstanceOperation
         ISingletonTest singleton, ITransientTest transient, IScopedTest scoped, ScopeAndTransientTester scopeAndTransientTester
            )
        {
            //OperationService = operationService;
            //TransientOperation = transientOperation;
            //ScopedOperation = scopedOperation;
            //SingletonOperation = singletonOperation;
            //SingletonInstanceOperation = singletonInstanceOperation;

            this.singleton = singleton;
            this.transient = transient;
            this.scoped = scoped;
            this.scopeAndTransientTester = scopeAndTransientTester;
        }

        [HttpGet]
        public string Get()
        {
            //return $"singleton={SingletonOperation.OperationId};transient={TransientOperation.OperationId};scoped={ScopedOperation.OperationId}";
            return $"singleton={singleton.guid}; \r\nscoped1={scoped.guid};\r\nscoped2={scopeAndTransientTester.ScopedID()};\r\ntransient={transient.guid};\r\ntransient2={scopeAndTransientTester.TransientID()};";
        }


    }
}