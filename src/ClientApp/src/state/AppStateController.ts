import { StateController, Action } from 'ajwahjs';
import axios from 'axios';
export class SuccessfullRegistered implements Action {
  type = 'SuccessfullRegistered';
}
type Todo = { id: number; title: string; completed: boolean };
export enum AsyncStatus {
  Idle,
  Pending,
  Success,
  Error,
}
type User = {
  firstName: string;
  lastName: string;
  userName: string;
  email: string;
  password: string;
  databaseName: string;
  isPayingUser: boolean;
  id: number;
  tenantId: string;
};
export interface AppState {
  todos: Todo[];
  users: User[];
  currentUser: User;
  asyncStatus: { status: AsyncStatus; message?: string };
}
export class AppStateController extends StateController<AppState> {
  constructor() {
    super({
      todos: [],
      users: [],
      currentUser: {} as any,
      asyncStatus: { status: AsyncStatus.Idle, message: '' },
    });
  }
  API_URL = 'http://localhost:4000/api/v1/';
  onInit() {
    this.loadUsers();
    this.getTodoList();
  }
  loadUsers() {
    axios.get(this.API_URL + 'Account/allUsers').then((res) => {
      this.emit({ users: res.data });
    });
  }
  register(user: User) {
    this.setStatus(AsyncStatus.Pending);
    axios.post(this.API_URL + 'Account/register', user).then((res) => {
      this.loadUsers();
      this.persistToen(res.data);
      this.setStatus(AsyncStatus.Success);
      this.setStatus(AsyncStatus.Idle);
    });
  }
  login(user: User) {
    this.setStatus(AsyncStatus.Pending);
    axios.post(this.API_URL + 'Account/login', user).then((res) => {
      if (res.data.message) {
        this.setStatus(AsyncStatus.Error, res.data.message);
        setTimeout(() => {
          this.setStatus(AsyncStatus.Idle);
        }, 2000);
        return;
      }
      this.persistToen(res.data);

      this.setStatus(AsyncStatus.Success);
      this.setStatus(AsyncStatus.Idle);
    });
  }
  setStatus(status: AsyncStatus, message = '') {
    this.emit({ asyncStatus: { status, message } });
  }
  persistToen(loginInfo: any) {
    localStorage.setItem('token', JSON.stringify(loginInfo));
    this.getTodoList();
  }
  getToken(): { token: string; databaseName: string; userName: string } {
    return JSON.parse(localStorage.getItem('token') || '{}');
  }
  getTodoList() {
    const token = this.getToken().token;
    if (!token) return;
    axios
      .get(this.API_URL + 'todo', {
        headers: { Authorization: token },
      })
      .then((res) => {
        this.emit({ todos: res.data });
      });
  }
  addTodo(todo: Todo) {
    const token = this.getToken().token;
    if (!token) return;
    axios
      .post(this.API_URL + 'todo', todo, {
        headers: { Authorization: token },
      })
      .then((res) => {
        this.getTodoList();
      });
    return true;
  }
  updateTodo(todo: Todo) {
    const token = this.getToken().token;
    if (!token) return;
    axios
      .put(this.API_URL + 'todo', todo, {
        headers: { Authorization: token },
      })
      .then((res) => {
        this.getTodoList();
      });
  }
}
