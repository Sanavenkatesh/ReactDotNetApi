const baseUrl = 'http://localhost:5000/api';
export const ApiServices = {
  register,
  login,
  getCookies
};

function register(FirstName, LastName, Email, Password) {
  const requestOptions = {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify({ FirstName, LastName, Email, Password })
  };
  return fetch(`${baseUrl}/Account/Register`, requestOptions);
}
function login(Email, Password, RememberMe) {
  const requestOptions = {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    credentials: 'include',
    body: JSON.stringify({ Email, Password, RememberMe })
  };
  return fetch(`${baseUrl}/Account/Login`, requestOptions);
}
function getCookies(cname) {
  const name = `${cname}=`;
  const decodedCookie = decodeURIComponent(document.cookie);
  const ca = decodedCookie.split(';');
  // eslint-disable-next-line no-plusplus
  for (let i = 0; i < ca.length; i++) {
    let c = ca[i];
    while (c.charAt(0) === ' ') {
      c = c.substring(1);
    }
    if (c.indexOf(name) === 0) {
      return c.substring(name.length, c.length);
    }
  }
  return '';
}
