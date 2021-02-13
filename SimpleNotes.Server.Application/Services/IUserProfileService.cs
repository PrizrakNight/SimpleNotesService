﻿using SimpleNotes.Server.Application.Models.Requests;
using SimpleNotes.Server.Application.Models.Responses;
using System.Threading.Tasks;

namespace SimpleNotes.Server.Application.Services
{
    public interface IUserProfileService
    {
        Task UpdateProfileAsync(UserProfileRequest profileRequest);

        Task<UserProfileResponse> GetProfileAsync();
    }
}
