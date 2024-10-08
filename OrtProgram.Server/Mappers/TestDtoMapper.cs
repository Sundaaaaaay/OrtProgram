using Microsoft.EntityFrameworkCore;
using OrtProgram.Server.DTO.Test;
using OrtProgram.Server.Entities;

namespace OrtProgram.Server.Mappers;

public static class TestDtoMapper
{
    public static ResponseTestDto ToResponseTestDto(this Test test)
    {
        return new ResponseTestDto
        {
            Id = test.Id,
            TestType = test.TestType,
            Description = test.Description,
            QuestionsAmount = test.QuestionsAmount
        };
    }
}