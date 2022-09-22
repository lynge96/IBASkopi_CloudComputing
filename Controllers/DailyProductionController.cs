﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DailyProduction.Model;
using Azure.Data.Tables;
using Azure;

namespace IbasAPI.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class DailyProductionController : ControllerBase
    {
        TableClient client;

        private List<DailyProductionDTO> _productionRepo;

        private readonly ILogger<DailyProductionController> _logger;


        public DailyProductionController(ILogger<DailyProductionController> logger)
        {
            _logger = logger;

            client = new TableClient(
                new Uri("https://ibasstorageacc.table.core.windows.net/IBASProduktion2020"),
                "IBASProduktion2020",
                new TableSharedKeyCredential("ibasstorageacc", "PIMKpwenxJ5VKfpMwJaR3DAYp4UeTvCMXerG7TOcoOTAjYuJx5ocTVBXW5qpNOGMJXZ48d9JJpgI+AStilpN7w=="));

            
           // client = new TableClient("DefaultEndpointsProtocol=https;AccountName=ibasstorageacc;AccountKey=PIMKpwenxJ5VKfpMwJaR3DAYp4UeTvCMXerG7TOcoOTAjYuJx5ocTVBXW5qpNOGMJXZ48d9JJpgI+AStilpN7w==;EndpointSuffix=core.windows.net", "IBASProduktion2020");
            

           /* _productionRepo = new List<DailyProductionDTO>
            {
                new DailyProductionDTO {Date = new DateTime(2020, 1, 31), Model = BikeModel.IBv1, ItemsProduced = 12},
                new DailyProductionDTO {Date = new DateTime(2020, 2, 28), Model = BikeModel.IBv1, ItemsProduced = 32},
                new DailyProductionDTO {Date = new DateTime(2020, 3, 31), Model = BikeModel.IBv1, ItemsProduced = 32},
                new DailyProductionDTO {Date = new DateTime(2020, 4, 30), Model = BikeModel.IBv1, ItemsProduced = 141},
                new DailyProductionDTO {Date = new DateTime(2020, 5, 31), Model = BikeModel.IBv1, ItemsProduced = 146},
                new DailyProductionDTO {Date = new DateTime(2020, 6, 30), Model = BikeModel.IBv1, ItemsProduced = 162},
                new DailyProductionDTO {Date = new DateTime(2020, 7, 31), Model = BikeModel.IBv1, ItemsProduced = 102},
                new DailyProductionDTO {Date = new DateTime(2020, 8, 31), Model = BikeModel.IBv1, ItemsProduced = 210},
                new DailyProductionDTO {Date = new DateTime(2020, 9, 30), Model = BikeModel.IBv1, ItemsProduced = 144},
                new DailyProductionDTO {Date = new DateTime(2020, 10, 31), Model = BikeModel.IBv1, ItemsProduced = 151},
                new DailyProductionDTO {Date = new DateTime(2020, 11, 30), Model = BikeModel.IBv1, ItemsProduced = 61},
                new DailyProductionDTO {Date = new DateTime(2020, 12, 31), Model = BikeModel.IBv1, ItemsProduced = 86},

                new DailyProductionDTO {Date = new DateTime(2020, 1, 31), Model = BikeModel.evIB100, ItemsProduced = 1},
                new DailyProductionDTO {Date = new DateTime(2020, 2, 28), Model = BikeModel.evIB100, ItemsProduced = 2},
                new DailyProductionDTO {Date = new DateTime(2020, 3, 31), Model = BikeModel.evIB100, ItemsProduced = 3},
                new DailyProductionDTO {Date = new DateTime(2020, 4, 30), Model = BikeModel.evIB100, ItemsProduced = 4},
                new DailyProductionDTO {Date = new DateTime(2020, 5, 31), Model = BikeModel.evIB100, ItemsProduced = 4},
                new DailyProductionDTO {Date = new DateTime(2020, 6, 30), Model = BikeModel.evIB100, ItemsProduced = 6},
                new DailyProductionDTO {Date = new DateTime(2020, 7, 31), Model = BikeModel.evIB100, ItemsProduced = 10},
                new DailyProductionDTO {Date = new DateTime(2020, 8, 31), Model = BikeModel.evIB100, ItemsProduced = 21},
                new DailyProductionDTO {Date = new DateTime(2020, 9, 30), Model = BikeModel.evIB100, ItemsProduced = 17},
                new DailyProductionDTO {Date = new DateTime(2020, 10, 31), Model = BikeModel.evIB100, ItemsProduced = 15},
                new DailyProductionDTO {Date = new DateTime(2020, 11, 30), Model = BikeModel.evIB100, ItemsProduced = 25},
                new DailyProductionDTO {Date = new DateTime(2020, 12, 31), Model = BikeModel.evIB100, ItemsProduced = 30},

                new DailyProductionDTO {Date = new DateTime(2020, 1, 31), Model = BikeModel.evIB200, ItemsProduced = 10},
                new DailyProductionDTO {Date = new DateTime(2020, 2, 28), Model = BikeModel.evIB200, ItemsProduced = 2},
                new DailyProductionDTO {Date = new DateTime(2020, 3, 31), Model = BikeModel.evIB200, ItemsProduced = 32},
                new DailyProductionDTO {Date = new DateTime(2020, 4, 30), Model = BikeModel.evIB200, ItemsProduced = 41},
                new DailyProductionDTO {Date = new DateTime(2020, 5, 31), Model = BikeModel.evIB200, ItemsProduced = 46},
                new DailyProductionDTO {Date = new DateTime(2020, 6, 30), Model = BikeModel.evIB200, ItemsProduced = 62},
                new DailyProductionDTO {Date = new DateTime(2020, 7, 31), Model = BikeModel.evIB200, ItemsProduced = 102},
                new DailyProductionDTO {Date = new DateTime(2020, 8, 31), Model = BikeModel.evIB200, ItemsProduced = 21},
                new DailyProductionDTO {Date = new DateTime(2020, 9, 30), Model = BikeModel.evIB200, ItemsProduced = 44},
                new DailyProductionDTO {Date = new DateTime(2020, 10, 31), Model = BikeModel.evIB200, ItemsProduced = 51},
                new DailyProductionDTO {Date = new DateTime(2020, 11, 30), Model = BikeModel.evIB200, ItemsProduced = 61},
                new DailyProductionDTO {Date = new DateTime(2020, 12, 31), Model = BikeModel.evIB200, ItemsProduced = 88}
            };*/
        }
        
        [HttpGet]
        public IEnumerable<DailyProductionDTO> Get()
        {
            List<DailyProductionDTO> nyListe = new List<DailyProductionDTO>();

            Pageable<TableEntity> entities2 = client.Query<TableEntity>();


            foreach (TableEntity items in entities2)
            {
                DailyProductionDTO cykelProd = new DailyProductionDTO();

                cykelProd.Date = DateTime.Parse(items.RowKey);

                if (int.Parse(items.PartitionKey) == 1)
                {
                    cykelProd.Model = BikeModel.IBv1;
                }
                else if (int.Parse(items.PartitionKey) == 2)
                {
                    cykelProd.Model = BikeModel.evIB100;
                }
                else if (int.Parse(items.PartitionKey) == 3)
                {
                    cykelProd.Model = BikeModel.evIB200;
                }

                cykelProd.ItemsProduced = items.GetInt32("itemsProduced");

                nyListe.Add(cykelProd);

            }

            return nyListe;
        }
    }
}
