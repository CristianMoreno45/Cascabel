Feature: Example
Framework example


@Functional

	Scenario: Successful form submission
	Given The user is in the web form located at <url>
	And enter first name '<firstName>', middle name '<middleName>' and last name '<lastName>'
	When he click on the send option
	Then the user will see the welcome message '<message>'
	And the web browser will be closed

	Examples: 
	| url                                      | firstName | middleName | lastName | message |
	| https://form.jotform.com/212346521647656 | Cristian  | Camilo     | Moreno   | Thank You! |

