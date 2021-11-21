import React from 'react';
import { AppStateController } from '../state/AppStateController';
import { Get } from 'ajwahjs';
import { useStream } from '../hooks';
import { map } from 'rxjs';
import { Table } from 'reactstrap';
export function Home() {
  const ctl = Get(AppStateController);
  const users = useStream(ctl.stream$.pipe(map((state) => state.users)), []);
  return (
    <Table bordered>
      <thead>
        <tr>
          <th>First Name</th>
          <th>Last Name</th>
          <th>User Name</th>
          <th>Passsword</th>
          <th>Database Name</th>
          <th>TenantId</th>
        </tr>
      </thead>
      <tbody>
        {users.map((u) => (
          <tr key={u.id}>
            <th>{u.firstName}</th>
            <td>{u.lastName}</td>
            <td>{u.userName}</td>
            <td>{u.password}</td>
            <td>{u.databaseName}</td>
            <td>{u.tenantId}</td>
          </tr>
        ))}
      </tbody>
    </Table>
  );
}
