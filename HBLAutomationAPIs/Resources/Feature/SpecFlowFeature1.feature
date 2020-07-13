Feature: SpecFlowFeature1
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers

@DemoAPI @GetCustomerByCNIC
Scenario Outline: Verify the response status of CRI Call for GetCustomerByCNIC
	Given the test case title is "<Case>"
    And the endpoint is "<endpoint>"
	When the API header is "<header>"
	And the queryparameter is "<queryparams>"
	And Get request is made

	@source:Data/HBLGet.xlsx
	Examples: 
	|Case|header|endpoint|queryparams|
