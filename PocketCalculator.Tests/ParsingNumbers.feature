Feature: ParsingNumbers
	In order to parse numbers
	I want to parse numbers

Scenario Outline: Parse a single number
	Given I have entered <input> into the calculator
	When I parse the input
	Then the result should be the number <result>
	Examples: 
	| input  | result |
	| 0      | 0      |
	| 1      | 1      |
	| 2      | 2      |
	| 3      | 3      |
	| 4      | 4      |
	| 5      | 5      |
	| 6      | 6      |
	| 7      | 7      |
	| 8      | 8      |
	| 9      | 9      |
	| 10     | 10     |
	| 123.45 | 123.45 |

Scenario Outline: Adding numbers
	Given I have entered <input> into the calculator
	When I parse the input
	Then the result should be the number <result>
	Examples: 
	| input    | result |
	| 0+0      | 0      |
	| 0+1      | 1      |
	| 1+0      | 1      |
	| 1+1      | 2      |
	| 1+2      | 3      |
	| 2+1      | 3      |
	| 2+2      | 4      |
	| 0.5+0.7  | 1.2    |
	| 3.1+7.11 | 10.21  |
	| 3.1+7.11+45 | 55.21  |

Scenario Outline: Subtracting numbers
	Given I have entered <input> into the calculator
	When I parse the input
	Then the result should be the number <result>
	Examples: 
	| input   | result |
	| 0-0     | 0      |
	| 0-1     | -1     |
	| 1-0     | 1      |
	| 1-1     | 0      |
	| 7.5-3.1 | 4.4    |
	| 11-3-1  | 7      |
	| 1-7-13  | -19    |

Scenario Outline: Adding and subtracting numbers
	Given I have entered <input> into the calculator
	When I parse the input
	Then the result should be the number <result>
	Examples: 
	| input   | result |
	| 0+0-0   | 0      |
	| 1+2-3   | 0      |
	| 7-3+11  | 15     |
	| 11-3+7  | 15     |
	| 1+3+5-7 | 2      |

Scenario Outline: Multiplying numbers
	Given I have entered <input> into the calculator
	When I parse the input
	Then the result should be the number <result>
	Examples: 
	| input | result |
	| 0*0   | 0      |
	| 0*1   | 0      |
	| 1*0   | 0      |
	| 1*1   | 1      |
	| 2*3   | 6      |
	| 7*13  | 91     |

Scenario Outline: Multiplying and Adding numbers
	Given I have entered <input> into the calculator
	When I parse the input
	Then the result should be the number <result>
	Examples: 
	| input | result |
	| 0*0+0   | 0      |
	| 2+1*0   | 2      |
	| 1*0+3   | 3      |
	| 1*1+4   | 5      |
	| 2+3*7   | 23     |
	| 7*13+5  | 96     |

	
Scenario Outline: Dividing numbers
	Given I have entered <input> into the calculator
	When I parse the input
	Then the result should be the number <result>
	Examples: 
	| input  | result |
	| 0/1    | 0      |
	| 1/1    | 1      |
	| 7/5    | 1.4    |
	| 1234/2 | 617    |

Scenario Outline: Negative numbers
	Given I have entered <input> into the calculator
	When I parse the input
	Then the result should be the number <result>
	Examples: 
	| input | result |
	| 0+-1  | -1     |
	| 0--1  | 1      |
	| 5*-1  | -5     |
	| 5/-2  | -2.5   |

Scenario Outline: Working with paranthesis
	Given I have entered <input> into the calculator
	When I parse the input
	Then the result should be the number <result>
	Examples: 
	| input         | result |
	| 3*(5+7)       | 36     |
	| 11*(5-7)      | -22    |
	| 1+(-2)        | -1     |
	| 1+(7*(3+5)-1) | 56     |

Scenario Outline: Working with percent
	Given I have entered <input> into the calculator
	When I parse the input
	Then the result should be the number <result>
	Examples: 
	| input   | result |
	| 5+10%   | 5.1    |
	| 120*20% | 24     |

Scenario Outline: Working with exponents
	Given I have entered <input> into the calculator
	When I parse the input
	Then the result should be the number <result>
	Examples: 
	| input   | result |
	| 5^2     | 25     |
	| 3^3     | 27     |
	| 9^0.5   | 3      |
	| 0.5^-1  | 2      |
	| 9^0     | 1      |
	| 0.25^-2 | 16      |

Scenario Outline: Working with Pi
	Given I have entered <input> into the calculator
	When I parse the input
	Then the result should be the number <result>
	Examples: 
	| input    | result |
	| \pi/\pi  | 1      |
	| (2*π)/(2*π) | 1      |
	| (2*\pi)/(\pi*2) | 1      |
	
Scenario Outline: Working with trigonometric operators
	Given I have entered <input> into the calculator
	When I parse the input
	Then the result should be the number <result>
	Examples: 
	| input         | result |
	| sin 0         | 0      |
	| sin(\pi)      | 0      |
	| sin (\pi/2)   | 1      |
	| sin (3*\pi/2) | -1     |
	| cos 0         | 1      |
	| cos (\pi)     | -1     |
	| cos (\pi/2)   | 0      |
	| cos (3*\pi/2) | 0      |
	| tan 0         | 0      |
	| tan \pi       | 0      |
