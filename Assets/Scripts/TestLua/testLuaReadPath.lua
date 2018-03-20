local class = {}

function main()

	class:init()
	return class
end

function class:init()
	print("init class")
end

function class:func()
	print("class func !")
end

function class.print( str ) --表的(类似静态)方法  不知道怎么使用
	if type(str) == 'string' then
		print("class print:%s",str)
	else
		print("class function print only accept string type")
	end
end