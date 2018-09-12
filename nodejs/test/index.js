/**
 * Test Runner
 *
 */

// Application logic for the test runner
const _app = {};

// Container for the test
_app.tests = {};

// Add unit tests
_app.tests.unit = require('./unit');

// Count all the tests
_app.countTests = () => {
  let counter = 0;
  for (const key in _app.tests) {
    if (_app.tests.hasOwnProperty(key)) {
      const subTests = _app.tests[key].default;
      for (const testName in subTests) {
        if (subTests.hasOwnProperty(testName)) {
          counter++;
        }
      }
    }
  }
  return counter;
};

// Run all the tests
_app.runTests = () => {
  let errors = [];
  let successes = 0;
  const limit = _app.countTests();
  let counter = 0;
  for (const key in _app.tests) {
    if (_app.tests.hasOwnProperty(key)) {
      const subTests = _app.tests[key].default;

      for (const testName in subTests) {

        if (subTests.hasOwnProperty(testName)) {
          (() => {
            const tmpTestName = testName;
            const testValue = subTests[testName];
            // Call the test
            try {
              testValue(() => {
                // If it calls back then it succeeded
                console.log('\x1b[32m%s\x1b[0m', `[v] ${tmpTestName}`);
                counter++;
                successes++;
                if (counter === limit) {
                  _app.produceReport(limit, successes, errors);
                }
              });
            } catch(error) {
              errors.push({
                name: testName,
                error,
              });
              console.log('\x1b[31m%s\x1b[0m', `[x] ${tmpTestName}`);
              counter++;
              if (counter === limit) {
                _app.produceReport(limit, successes, errors);
              }
            }
          })();
        }
      }
    }
  }
};

// Produce a test report
_app.produceReport = (limit, successes, errors) => {
  console.log('');
  console.log('------------------BEGIN TESTS------------------');
  console.log('');
  console.log('Total tests: ', limit);
  console.log('Pass: ', successes);
  console.log('Fails: ', errors.length);
  console.log('');

  // Error details
  if (errors.length > 0) {
    console.log('\x1b[31m%s\x1b[0m', 'ERROR DETAILS:');
    errors.forEach(function(testError) {
      console.log('\x1b[31m%s\x1b[0m', '* ' + testError.name);
      console.log('  ', testError.error);
      console.log('');
    });
  }

  console.log('');
  console.log('-------------------END TESTS-------------------');
  process.exit(0);
};

// Run the tests
_app.runTests();
