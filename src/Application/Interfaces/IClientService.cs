﻿using Application.Models.Requests;
using Application.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IClientService
    {
        ClientDTO CreateNewClient(CreateClientDTO ClientDto);

        List<ClientDTO> GetAllClients();


        void UpdateClient(UpdateClientDTO ClientDto);


    }
}
