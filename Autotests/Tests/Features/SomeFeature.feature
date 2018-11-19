Feature: SomeFeature
	I read several test scenario,
	And I want the automatic tests to run them.

@test
Scenario Outline: Do Some Scenario
	Given I open browser and go to website page
	Then I open the yandex market section
	And Choose a subsection as a <subsection>
	Then I open advanced search on the page
	And set the search parameter as <price>
	And I also choose manufacturers as <manufacturers>
	Then I submit changes
	And check that the elements on page twelve
	Then I memorize the first item in the list
	And then I insert the saved name into the search field
	Then I check that the name of the product matches the previously saved name

	Examples: 
	| subsection  | price | manufacturers      |
	| smartphones | 20000 | iphone and samsung |
	| headphones  | 5000  | beats              |

	@test
Scenario Outline: CheckSorted
	Given I open browser and go to website page
	Then I open the yandex market section
	And Choose a subsection as a <subsection>
	Then I click the Sort by price button
	And check that the items on the page are sorted correctly

	Examples: 
	| subsection  |
	| smartphones |