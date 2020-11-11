using System.Collections.Generic;
using AutoMapper;
using FlowerStoreAPI.Dtos;
using FlowerStoreAPI.Dtos.FlowerDTOS;
using FlowerStoreAPI.Models;
using FlowerStoreAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;


namespace FlowerStoreAPI.Controllers
{   
    [Route("api/flowers")]
    [ApiController]
    public class FlowersController : ControllerBase
    {
        private readonly IFlowerRepo _repository;
        private readonly IMapper _mapper;
        
        public FlowersController(IFlowerRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }


        //GET api/flowers
        [HttpGet]
        public async Task<ActionResult <IEnumerable<FlowerReadDto>>> GetAllFlowers()
        {
            var flowerItems = await _repository.GetAllFlowers();

            return Ok( _mapper.Map<IEnumerable<FlowerReadDto>>(flowerItems));
        }


        //GET api/flowers/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult <FlowerReadDto>> GetFlowerById(int id)
        {
            var flowerItem = await _repository.GetFlowerById(id);
            if(flowerItem != null)
            {
                return Ok( _mapper.Map<FlowerReadDto>(flowerItem));
            }
            return NotFound();

        }


        //POST api/flowers
        [HttpPost]
        public async Task<ActionResult <FlowerReadDto>> CreateFlower(FlowerCreateDto flowerCreateDto){

            var flowerModel = _mapper.Map<Flower>(flowerCreateDto);
            _repository.CreateFlower(flowerModel);
            _repository.SaveChanges();

            var flowerReadDto = _mapper.Map<FlowerReadDto>(flowerModel);

            return CreatedAtRoute(nameof(GetFlowerById), new{Id = flowerReadDto. Id}, flowerReadDto);
        }


        //PUT api/flowers/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateFlower(int id, FlowerUpdateDto flowerUpdateDto)
        {
            var flowerModelFromRepo = await _repository.GetFlowerById(id);
            if(flowerModelFromRepo == null)
            {
                return NotFound();
            }

            _mapper.Map(flowerUpdateDto, flowerModelFromRepo);

            _repository.UpdateFlower(flowerModelFromRepo);

            _repository.SaveChanges();

            return NoContent();
        }


        //DELETE api/flowers/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteFlower(int id)
        {
            var flowerModelFromRepo = await _repository.GetFlowerById(id);
            if(flowerModelFromRepo == null)
            {
                return NotFound();
            }
            _repository.DeleteFlower(flowerModelFromRepo);
            _repository.SaveChanges();

            return NoContent();
        }
    }
}

    

