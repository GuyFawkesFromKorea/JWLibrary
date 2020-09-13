﻿using FluentValidation;
using JWLibrary.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JWLibrary.Pattern.TaskAction {
    public class ValidateTaskAction<TIAction, TAction, TRequest, TResult, TValidator> : TaskAction<TIAction, TAction, TRequest, TResult>
        where TIAction : IActionBase<TRequest, TResult>
        where TAction : ActionBase<TRequest, TResult>, new()
        where TRequest : class
        where TValidator : class, IValidator<TAction>, new() {
        protected TValidator _validator;

        private readonly static Lazy<HashSet<TValidator>> _validations = new Lazy<HashSet<TValidator>>(() => {
            return new HashSet<TValidator>();
        });

        public ValidateTaskAction() {
            _validator = _validations.Value.jFirst(m => m.GetType().jEquals(typeof(TValidator)));
            _validator.jIfNull(_ => {
                _validator = new TValidator();
                _validations.Value.Add(_validator);
            });
        }

        public override Task<TResult> ExecuteCore() {
            var validateResult = _validator.Validate(this._instance);
            if (validateResult.IsValid.jIsFalse()) throw new Exception(validateResult.Errors[0].ErrorMessage);
            return base.ExecuteCore();
        }
    }
}