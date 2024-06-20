grammar Grammar;

compileUnit: EOF;

program: line* EOF;

line: statement | printStatement | ifBlock | whileBlock | classDeclaration | methodDeclaration | classCall;

statement
    : assignment ';'
	| variableDeclaration ';'
    | functionCall ';'
    | printStatement ';'
    | whileBlock      
    | ifBlock
    | returnStatement ';'
    ;

printStatement : 'PRINT' '(' expression ')' ;

variableDeclaration : type IDENTIFIER '=' expression ;

assignment : IDENTIFIER funkyOp expression?;

functionCall: methodName '(' arguments? ')' ;

arguments : expression (',' expression)* ;

ifBlock: 'if' expression block ('else' elseIfBlock)?;

elseIfBlock: block | ifBlock ;

whileBlock: WHILE expression block ;

WHILE: 'while' | 'until';

expression: 
    constant							# constantExpression
    | IDENTIFIER						# idExpression
    | functionCall						# functionExpression
    | classFunctionCall					# classExpression
    | '(' expression ')'				# paranthesesExpression
    | '!' expression					# notExpression
    | expression multOp expression		# multExpression
    | expression addOp expression		# addExpression
    | expression compareOp expression	# compareExpression
    | expression boolOp expression		# boolExpression
	;
    
funkyOp: '=' | '%%' | '$$' | '@@' |  '--' | '+=' |  '-=' | '+++' | '---' |  '/=' | '*=' | '%=' | '^=' |  '++';

multOp: '*' | '/' | '%';
addOp: '+' | '-' ;
compareOp: '==' | '!=' | '>' | '<' | '>=' | '<=';
boolOp: BOOL_OPERATOR;

BOOL_OPERATOR: 'and' | 'or' | 'xor';

constant: INTEGER | FLOAT | STRING | BOOL | NULL;

INTEGER: '-'?[0-9]+;
FLOAT: '-'?[0-9]+ '.' [0-9]+;
STRING: '"' ~'"'* '"';
BOOL: 'true' | 'false';
NULL: 'null';

block: '{' line* '}';

classDeclaration: 'class' className '{' classBody '}';

className: IDENTIFIER;

classBody: classMember*;

classMember: fieldDeclaration | methodDeclaration;

classCall: className IDENTIFIER '=' 'new' className'('')'';'; 

classFunctionCall: IDENTIFIER '.' methodName'(' (expression (',' expression)*)? ')' ;

fieldDeclaration: type fieldName ';';

fieldName: IDENTIFIER;

methodDeclaration: type methodName '(' parameterList? ')' methodBody;

methodBody: '{' statement* '}' ;

methodName: IDENTIFIER;

parameterList: parameter (',' parameter)*;

parameter: type parameterName;

parameterName: IDENTIFIER;

type: 'int' | 'float' | 'string' | 'bool' | 'void' | className;

returnStatement: 'return' expression? ;

WS: [ \t\r\n]+ -> skip;
IDENTIFIER: [a-zA-Z_][a-zA-Z0-9_]*;
