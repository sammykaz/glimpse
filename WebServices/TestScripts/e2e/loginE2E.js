describe('Login Page', function() {
  it('should login using the vendor leglimpse', function() {
    browser.get('http://localhost:55709/');
    element(by.model('user.username')).sendKeys('leglimpse@gmail.com');
    element(by.model('user.password')).sendKeys('eeXQ5d7yXDJ7mx4g7Y5n/w==');
   
    element(by.buttonText("Login")).click();
    expect(browser.getCurrentUrl()).toContain('promotions');
  });
});