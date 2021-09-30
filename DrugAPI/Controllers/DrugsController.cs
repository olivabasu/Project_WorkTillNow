using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DrugAPI.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DrugAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DrugsController : ControllerBase
    {
        
        IDrugRepository _drug;
        public DrugsController(IDrugRepository drug)
        {
            _drug = drug;
            
        }
        /// <summary>
        /// This method responsible for returing the Drug Details searched by Drug ID
        /// </summary>
        /// <param name="drug_id"></param>
        /// <returns></returns>

        [HttpGet("GetDrugDetails/{drug_id}")]
        public IActionResult GetDrugDetails(int drug_id)
        {
            

            try
            {
                var obj = _drug.searchDrugsByID(drug_id);
                if (obj == null)
                {
                    return NotFound();
                }

                return Ok(obj);
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }
        /// <summary>
        /// This method responsible for returing the Drug Details searched by Drug Name
        /// </summary>
        /// <param name="drug_name"></param>
        /// <returns></returns>

        [HttpGet("GetDrugDetailByName/{drug_name}")]
        public IActionResult GetDrugDetailByName(string drug_name)
        {
           
            try
            {
                var obj = _drug.searchDrugsByName(drug_name);
                if (obj == null)
                {
                    return NotFound();
                }

                return Ok(obj);
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }
        /// <summary>
        /// This method responsible for returing the Drug Details searched by Drug ID and Location
        /// </summary>
        /// <param name="drug_id"></param>
        /// <param name="drug_loc"></param>
        /// <returns></returns>

        [HttpGet("{id}/{loc}")]
        public IActionResult getDispatchableDrugStock(int id, string loc)
        {
            
            var drug = _drug.GetDispatchableDrugStock(id,loc);
            if (drug == null)
                return null;
            return Ok(drug);
        }


    }
}