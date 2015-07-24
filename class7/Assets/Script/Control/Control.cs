using GDGeek;
using UnityEngine;

public class Control : MonoBehaviour 
{
    private FSM fsm = new FSM();

    public View _view;
    public Model _model;
	
	void Start () 
	{
        //状态的注册
	    fsm.addState("begin",BeginState());
        fsm.addState("play",PlayState());

        fsm.addState("input", InputState(),"play");
        fsm.addState("fall", FallState(), "play");
        fsm.addState("remove", RemoveState(), "play");


        fsm.addState("end",EndState());

        //开始进入的状态，即状态的初始化
//        fsm.init("input");              //如果直接初始化状态，方块将显示不出来，可能与计算机运行速率有关
        Invoke("test",0.1f);        //为了是在直接进入输入状态时可以显示方块，
	}
     
    private void test()
    {
        fsm.init("input");
        
    }
    /// <summary>
    /// 输入对应的列号，然后在对应的列下落
    /// </summary>
    /// <param name="x"></param>
    /// <param name="number"></param>
    private void Input(int x,int number)
    {
        Cube c = _model.GetCube(0, 0);
        c.isEnable = false;
        c = _model.GetCube(x, 0);
        c.isEnable = true;
        c.number = number;
//        for (int y = _model.height - 1; y >= 0; --y)
//        {
            //获取最上端的方块

           

//            Cube c = _model.GetCube(x, y);
//            if (!c.isEnable)
//            {
//                c.isEnable = true;
//                c.number = Random.Range(3, 8);
//                break;
//            }
//        }
        RefreshModelToView();
    }

    //输入状态，在输入开始时显示一个数字
    private State InputState()
    {
        StateWithEventMap state = new StateWithEventMap();
        int number = 0;
        state.onStart += delegate
        {
            
            number = Random.Range(3, 8);
            Cube c = _model.GetCube(0, 0);  //在0，0位置出现数字
            c.number = number;
            c.isEnable = true;
            RefreshModelToView();          //每次操作完数据要在界面上画出来
            
        };
        //根据状态事件出发某个执行动作
        state.addAction("1", delegate(FSMEvent evt)
        {
            Input(0,number);
            return "fall";  //当动作执行完后返回另一个状态,输入为空时返回本状态
        });

        state.addAction("2", delegate(FSMEvent evt)
        {
            Input(1,number);
            return "fall";

        });

        state.addAction("3", delegate(FSMEvent evt)
        {
            Input(2,number);
            return "fall";

        });

        state.addAction("4", delegate(FSMEvent evt)
        {
            Input(3,number);
            return "fall";

        });
        return state;
    }

    

    //下落状态，下落完成后移动到移出状态
    private State FallState()
    {
        StateWithEventMap state = TaskState.Create(
            delegate
            {
//                TaskWait task = new TaskWait();
//                task.setAllTime(0.5f);      //在任务开始之前等待0.5f
//                //在下落之前等待五秒然后下落
//                TaskManager.PushBack(task, delegate
//                {
//                    DoFall();
//                });

                //直接执行一个任务
                Task task = DoFall();
                return task;
            },fsm,"remove"
            );

        return state;
    }

   

    //移出状态
    private State RemoveState()
    {
        bool flag = false;
        StateWithEventMap state = TaskState.Create(
            delegate
            {
                Task task = new Task();
                
                //在进行此任务之前先检查消除，执行完检查消除后才会执行此状态
                TaskManager.PushFront(task, delegate
                {
                    flag = CheckAndRemove();
                });
                return task;
            },
            fsm,
            delegate //使用委托返回下一个状态
            {
                return flag ? "fall" : "input";
            }
            );
        return state;
    }

    /// <summary>
    /// 下落，从下往上检查，当检查到有方块显示时，开始让他从上往下下落到底部
    /// 将下落过程做为一个任务来执行
    /// </summary>
    private Task DoFall()
    {
        TaskSet ts = new TaskSet();
        Cube end = null;
        int endY = 0;
        for (int i = 0; i < _model.width; i++)
        {
            for (int j = _model.height - 1; j >= 0; --j)
            {
                Cube c = _model.GetCube(i, j);
                if (c.isEnable)
                {
                    for (int k = j + 1; k < _model.height; ++k)
                    {
                        Cube f = _model.GetCube(i, k);
                        if (f == null || f.isEnable)
                        {
                            break;
                        }
                        else
                        {
                            end = f;
                            endY = k;
                           
                        }
                    }
                    if (end != null)
                    {
                        end.number = c.number;
                        end.isEnable = true;
                        c.isEnable = false;
                        ts.push(_view.play.MoveTask(c.number,new Vector2(i,j),new Vector2(i,endY)));
                    }
                }
            }
        }

        //在任务集全部执行完毕后在刷新界面
        TaskManager.PushBack(ts, delegate
        {
            RefreshModelToView();            
        });
        return ts;
    }
    /// <summary>
    /// 检查消除符合条件的模块
    /// </summary>
    private bool CheckAndRemove()
    {
        bool flag = false;
        for (int i = 0; i < _model.width; i++)
        {
            for (int j = 0; j < _model.height; j++)
            {
                Cube c = _model.GetCube(i, j);

                if (c.isEnable)
                {
                    Cube up = _model.GetCube(i, j - 1);
                    if (up != null && up.isEnable && up.number + c.number == 10)
                    {
                        up.isEnable = false;
                        c.isEnable = false;
                        flag = true;
                        break;
                    }

                    Cube down = _model.GetCube(i, j + 1);
                    if (down != null && down.isEnable && down.number + c.number == 10)
                    {
                        down.isEnable = false;
                        c.isEnable = false;
                        flag = true;
                        break;
                    }

                    Cube left = _model.GetCube(i - 1, j);
                    if (left != null && left.isEnable && left.number + c.number == 10)
                    {
                        left.isEnable = false;
                        c.isEnable = false;
                        flag = true;
                        break;
                    }

                    Cube right = _model.GetCube(i + 1, j);
                    if (right != null && right.isEnable && right.number + c.number == 10)
                    {
                        right.isEnable = false;
                        c.isEnable = false;
                        flag = true;
                        break;
                    }
                }
              
            }
        }

        RefreshModelToView();
        return flag;
    }


    /// <summary>
    /// 使用有限状态机的post功能给状态机发送消息
    /// </summary>
    /// <param name="massage"></param>
    public void PostMsg(string massage)
    {
//        Debug.Log(massage);
        fsm.post(massage);
    }

    private State BeginState()
    {
        StateWithEventMap state = new StateWithEventMap();
        state.onStart += delegate { _view.begin.gameObject.SetActive(true); };
        state.onOver += delegate { _view.begin.gameObject.SetActive(false); };
        state.addEvent("begin", "input");

        return state;
    }
    private State PlayState()
    {
//        //创建一个任务状态,任务的执行需要一个任务管理器，在视图界面里建立
//        StateWithEventMap state = TaskState.Create(delegate   
//        {
//            //创建一个等待任务
//            TaskWait ts = new TaskWait();
//            //等待时间
//            ts.setAllTime(3f);
//            return ts;
//        }, 
//        fsm, //属于哪个有限状态机
//        "end"); //当任务完成时进入的下一个状态

        StateWithEventMap state = new StateWithEventMap();
        state.onStart += delegate { _view.play.gameObject.SetActive(true); };
        state.onOver += delegate { _view.play.gameObject.SetActive(false); };

        //RefreshModelToView();
        return state; 
    }

    /// <summary>
    /// 将模型层的数据同步到显示层
    /// </summary>
    private void RefreshModelToView()
    {
        for (int i = 0; i < _model.width; i++)
        {
            for (int j = 0; j < _model.height; j++)
            {
                Cube c = _model.GetCube(i, j);
                Square s = _view.play.GetSquare(i, j);

                if (c.isEnable)
                {
                    Debug.Log(i +":" + j + c.isEnable);
                    s.Show();
                    s.Number = c.number.ToString();
                }
                else
                {
                    s.Hide();
                }
            }
        }
    }


    private State EndState()
    {
        //带消息映射表的状态类
        StateWithEventMap state = new StateWithEventMap();
        state.onStart += delegate { _view.end.gameObject.SetActive(true); };
        state.onOver += delegate { _view.end.gameObject.SetActive(false); };

        state.addEvent("end", "begin");

        return state;
    }
   
	
}
