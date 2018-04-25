from setuptools import setup

setup(
    name='blockchain',
    include_package_data=True,
    install_requires=[
        'flask',
        'requests',
    ],
    setup_requires=[
        'pytest-runner',
    ],
    tests_require=[
        'pytest',
    ],
)