﻿using CustomApi.Business.ExternalServices.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CustomApi.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    IEmailService _emailService;

    // GET: api/<AuthController>
    [HttpGet]
    public IEnumerable<string> Get()
    {
        return new string[] { "value1", "value2" };
    }

    // GET api/<AuthController>/5
    [HttpGet("{id}")]
    public string Get(int id)
    {
        return "value";
    }

    // POST api/<AuthController>
    [HttpPost] // x2 register, login
    public void Post([FromBody] string value)
    {
    }

    // PUT api/<AuthController>/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] string value)
    {
    }

    // DELETE api/<AuthController>/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }
}