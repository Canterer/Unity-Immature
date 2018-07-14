-- View -> Action -> Model -> State ->View

View = {}
View.ready = function(model)
	print(model.counter)
end

Action = {}
Action.decrement = function(data, present)
	present = present or model.present
	data = data or {}
	data.counter = data.counter or 10
	setTimeout(function(){
		
	}
	end
end

Model = {
	counter = 1,
	started = 1,
}
Model.present = function(data)
	if state.counting(model) then
		if( model.counter == 0 ) then
			model.launched = data.launched
		else
			model.aborted = data.aborted
			model.counter = data.counter
		end
	end
end

State = {}
State.nextAction = function(model)
	if state.counting(model) then
		if model.counter == 0 then
			action.decrement({counter = model.counter},model.present)
		end
		if model.counter == 1 then
			action.launch({},model.present)
		end
end
State.representation = function(model)
	if state.ready(model) then
		representation = state.view.ready(model);
	end
end

--action 就是改变状态的指令、操作
--action 接受Model参数，用于传递数据给Model

--model:类似保存状态
--view:根据model状态来更新显示
--state:类似状态机，复杂调控action的运行
--总结:
--model由系统的所有属性及其可能的值组成
--状态则指定所启用的Action，她会给定一组值。

-- SAM模式:状态-行为-模型，State-Action-Model
-- V = S( vm(M.present(A(data))), nap(M))
-- A(Action)、vm(视图-模型，view-model)、nap(next-action断言)、S(状态表述) 均为纯函数
-- “状态”(系统中属性的值)要完全局限于Model之中，改变这些值的逻辑在Model本身之外是不可见的。
-- 应用一个Action A之后，View V可以计算得出，Action会作为Model的纯函数。

-- -> Action -> Model -> State -> View
-- -> View -> Action
-- -> State -> Action
-- Model被Model或Action调用，State只有Model产出  View只被State更新

-- 优缺点:
-- View的所有元素必须存在 Model可能保存巨大的状态数据
-- 逻辑稍稍复杂的界面，将会Yoon大量的State

-- 变量化处理预制体中的元素
-- 为每一个需要处理的界面元素提供State语句控制
-- 为每一个界面元素的反馈组成Action,将游戏中的数据变动、时间变动、消息传递组成Action
-- 将界面的每一次的变化 组合State们成一个Model的状态

action = function(data,model)
	model.data = data
end

state = function(data,view)
end

vm = 监听model中数据变化 自动整理变动数据以及回调函数,并调用state函数，类似状态机

View_Model = View_Model or {}

View_Model.model = {}
View_Model.model.vm = View_Model
View_Model.view = nil

Action_Load = function(data,)
end

Action = function( data, View_Model.model )
	for k,v in pairs(data.resList) do
		print(k,v)
	end
	Action_Load(data,View_Model.model)
end