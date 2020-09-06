﻿using JWActions.WeatherForecast;
using JWLibrary.ApiCore.Base;
using JWLibrary.Core;
using JWLibrary.Pattern.TaskAction;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JWLibrary.ApiCore.Controllers {

    /// <summary>
    /// CRUD TEST CLASS
    /// </summary>
    public class WeatherForecastController : JControllerBase {
        private readonly ILogger<WeatherForecastController> _logger;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="logger"></param>
        public WeatherForecastController(ILogger<WeatherForecastController> logger) {
            _logger = logger;
        }

        /// <summary>
        /// get
        /// </summary>
        /// <param name="idx"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<WEATHER_FORECAST> Get(int idx = 1) {
            WEATHER_FORECAST result = null;
            using (var action = ActionFactory.CreateAction<IGetWeatherForecastAction,
                        GetWeatherForecastAction,
                        WeatherForecastRequestDto,
                        WEATHER_FORECAST,
                        GetWeatherForecastAction.Validator>()) {
                action.Request = new WeatherForecastRequestDto() {
                    ID = idx
                };
                result = await action.ExecuteCore();
            }
            return result;
        }

        /// <summary>
        /// get all
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<WEATHER_FORECAST>> GetAll() {
            IEnumerable<WEATHER_FORECAST> result = null;
            using (var action = ActionFactory.CreateAction<IGetAllWeatherForecastAction, GetAllWeatherForecastAction, WeatherForecastRequestDto, IEnumerable<WEATHER_FORECAST>>()) {
                result = await action.ExecuteCore();
            }
            return result;
        }

        /// <summary>
        /// Post메서드
        /// </summary>
        /// <param name="request">요청:RequestDto<TestRequestDto></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<int> Save([FromBody][ModelBinder(typeof(JPostModelBinder<RequestDto<WEATHER_FORECAST>>))]
            RequestDto<WEATHER_FORECAST> request) {
            using (var action = ActionFactory.CreateAction<ISaveWeatherForecastAction, SaveWeatherForecastAction, WEATHER_FORECAST, int, SaveWeatherForecastAction.Validator>()) {
                action.Request = request.Dto;
                return await action.ExecuteCore();
            }
        }

        /// <summary>
        /// remove
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> Remove([FromBody][ModelBinder(typeof(JPostModelBinder<RequestDto<WeatherForecastRequestDto>>))]
            RequestDto<WeatherForecastRequestDto> request) {
            using (var action = ActionFactory.CreateAction<IDeleteWeatherForecastAction, DeleteWeatherForecastAction, WeatherForecastRequestDto, bool>()) {
                action.Request = request.Dto;
                return await action.ExecuteCore();
            }
        }
    }
}