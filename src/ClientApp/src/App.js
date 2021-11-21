import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { Register } from './components/Register';
import { TodoList } from './components/TodoList';
import { Login } from './components/Login';
import './custom.css';

export default class App extends Component {
  static displayName = App.name;

  render() {
    return (
      <Layout>
        <Route exact path="/" component={Home} />
        <Route path="/register" component={Register} />
        <Route path="/todo" component={TodoList} />

        <Route path="/login" component={Login} />
      </Layout>
    );
  }
}
