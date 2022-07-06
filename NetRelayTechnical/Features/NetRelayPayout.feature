Feature: NetRelayPayout
One of the APIs Nets provides to its customers is the Reporting API. 
For the purpose of this exercise, we have implemented a mock of a (much) simplified version of this API. 
The API reference for the Simplified Reporting API


@regression
Scenario Outline: Verify connectivity response for EndPoints
	Given a GET request is made to payouts EndPoint with status code '<endPoint>'
	Then verify a status <statusCode> response code

		Examples: 
				| endPoint                                              | statusCode          |
				| report/v1/payouts                                     | OK                  |
				| report/v1/payouts?id=11ebb9ef6a7d4df0b20d59ad574e9761 | OK                  |
				| report/v2/payouts                                     | InternalServerError |
	
@regression
Scenario: Verify Charged Amount accepts only integer value for Endpoint1
	Given a GET request is made to payouts EndPoint One 'report/v1/payouts'
	Then Verify charged amount is Integer value for EndPoint One

@bug @ignore
Scenario: Verify Charged Amount accepts only integer value for EndPoint2
	Given a GET request is made to payouts EndPoint Two 'report/v1/payouts?id=11ebb9ef6a7d4df0b20d59ad574e9761'
	Then Verify charged amount is Integer value for Endpoint Two

	@regression
Scenario: Verify paymentAction is not empty and contain possible values
	Given a GET request is made to payouts EndPoint Two 'report/v1/payouts?id=11ebb9ef6a7d4df0b20d59ad574e9761'
	Then Verify payment action is not empty and contains <value>
	Examples: 
				| value  |
				| CHARGE |
@regression
Scenario: Verify Number of Payouts
	Given a GET request is made to payouts EndPoint One 'report/v1/payouts'
	Then verify number of payouts is greater than one

@regression
Scenario: Verify payouts required response data for the First data in the array
	Given a GET request is made to payouts EndPoint One 'report/v1/payouts'
	Then Verify required response data
	

	