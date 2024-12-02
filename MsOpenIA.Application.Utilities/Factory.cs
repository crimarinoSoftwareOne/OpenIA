namespace MsOpenIA.Application.Utilities
{
    using MsOpenIA.Domain.Entities;
    using MsOpenIA.Domain.Interfaces.Utilities;
    using OpenAI.Chat;
    using System;
    using System.Text.Json;

    public class Factory : IFactory
    {
        private readonly string _nameFunction = "analyze_and_suggest_code_solutions";

        #region Function
        public string NameFunction() => _nameFunction;

        public ChatTool GetFunctionTool()
        {
            return ChatTool.CreateFunctionTool(
                functionName: _nameFunction,
                functionDescription: "Analyzes provided code snippets and suggests solutions along with alternative solutions.",
                functionParameters: BinaryData.FromBytes("""
                {
                    "type": "object",
                    "properties": {
                        "solutionDescription": {
                            "type": "string",
                            "description": "Description of the proposed solution"
                        },
                        "solutionCode": {
                            "type": "string",
                            "description": "Code for the proposed solution"
                        },
                        "timeToFix": {
                            "type": "string",
                            "description": "Estimated time to fix the issue"
                        },
                        "alternativeSolutions": {
                            "type": "array",
                            "description": "List of two alternative solutions",
                            "items": {
                                "type": "object",
                                "required": [
                                    "solutionDescription",
                                    "solutionCode",
                                    "timeToFix"
                                ],
                                "properties": {
                                    "solutionDescription": {
                                        "type": "string",
                                        "description": "Description of the alternative solution"
                                    },
                                    "solutionCode": {
                                        "type": "string",
                                        "description": "Code for the alternative solution"
                                    },
                                    "timeToFix": {
                                        "type": "string",
                                        "description": "Estimated time to implement the alternative solution"
                                    }
                                },
                                "additionalProperties": false
                            }
                        }
                    },
                    "additionalProperties": false,
                    "required": [
                        "solutionDescription",
                        "solutionCode",
                        "timeToFix",
                        "alternativeSolutions"
                    ]
                }
                """u8.ToArray())
                    );
        }
        #endregion

        #region DTO
        public ModelOpenAI DtoToModelOpenAI(dynamic obj)
        {
            string jsonString = JsonSerializer.Serialize(obj);
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            return JsonSerializer.Deserialize<ModelOpenAI>(jsonString, options);
        }

        public dynamic ModelOpenAIToDto(object model)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            string jsonString = JsonSerializer.Serialize(model, options);
            return JsonSerializer.Deserialize<dynamic>(jsonString, options);
        }
        #endregion

        #region BuildMessages
        public string BuildSystemChatMessage()
        {
            return $"Analyze the provided code snippet, identify issues, and use the function to generate structured suggestions for code improvements." +
                $"#Steps " +
                $"1. **Analyze the Code**: Identify logic errors, inefficiencies, or potential issues." +
                $"2. **Generate the Best Solution**: " +
                    $"- **solutionDescription**: Explain the identified problem and the proposed solution." +
                    $"- **solutionCode**: Provide the corrected or improved code version with comments." +
                    $"- **timeToFix**: Estimate the time required to implement the solution." +
                $"3. **Propose Two Alternative Solutions**: " +
                    $"- **alternativeSolutions**: Offer two alternative methods to solve the issue." +
                    $"- Include the same parameters as the primary solution, but without further alternatives." +
                $"Use the ` {_nameFunction} ` function to map and return your response.";
        }

        public string BuildIssueDescriptionMessage(string issueDescription)
        {
            return $"The error message is: {issueDescription} .";
        }

        public string BuildLineCodeMessage(string lineCode)
        {
            return $"Here is the code snippet: {lineCode} .";
        }
        #endregion

        public ResponseAI ConvertJsonToResponseAI(JsonElement obj)
        {
            return JsonSerializer.Deserialize<ResponseAI>(obj.GetRawText());
        }

        public string SerializeObject<T>(T obj)
        {
            return JsonSerializer.Serialize<T>(obj);
        }
    }
}
