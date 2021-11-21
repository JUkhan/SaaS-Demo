import { Get } from 'ajwahjs';
import { AppStateController } from '../state/AppStateController';
import {
  Alert,
  ListGroup,
  ListGroupItem,
  Input,
  Button,
  Row,
  Col,
} from 'reactstrap';
import { useStream } from '../hooks';
import { map } from 'rxjs';
import { useState } from 'react';
export function TodoList() {
  const ctrl = Get(AppStateController);
  const { databaseName, userName } = ctrl.getToken();
  const todos = useStream(
    ctrl.stream$.pipe(map((state) => state.todos)),
    ctrl.state.todos
  );
  const [val, setVal] = useState('');
  return (
    <div>
      <Alert color="primary">
        Database Name: <b>{databaseName}</b> User Name: <b>{userName}</b>
      </Alert>
      <Row>
        <Col>
          <Input
            value={val}
            onChange={(el) => setVal(el.target.value)}
            type="text"
          />
        </Col>
        <Col>
          <Button
            onClick={(el) => val && ctrl.addTodo({ title: val }) && setVal('')}
            type="button"
          >
            Add Todo
          </Button>
        </Col>
      </Row>
      <ListGroup>
        {todos.map((todo) => (
          <ListGroupItem key={todo.id}>
            <Input
              type="checkbox"
              checked={todo.completed}
              onChange={(el) =>
                ctrl.updateTodo({ ...todo, completed: el.target.checked })
              }
            />
            <span className="ms-2">{todo.title}</span>
          </ListGroupItem>
        ))}
      </ListGroup>
    </div>
  );
}
